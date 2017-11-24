// rectselect.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include <iostream>
#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include <cv.h>  
#include<highgui.h>
using namespace cv;
using namespace std;

double angle( Point pt1, Point pt2, Point pt0 ) {// finds a cosine of angle between vectors from pt0->pt1 and from pt0->pt2
    double dx1 = abs(pt1.x - pt0.x);
    double dy1 = abs(pt1.y - pt0.y);
    double dx2 = abs(pt2.x - pt0.x);
    double dy2 = abs(pt2.y - pt1.y);
    double ratio;//�߳�ƽ���ı�
    ratio=(dx1*dx1+dy1*dy1)/(dx2*dx2+dy2*dy2);
    if (ratio<0.8 || 1.2<ratio) {//���ݱ߳�ƽ���ıȹ�С�������ǰ��̭����ı��Σ������̭���࣬�����˱�������
//      Log("ratio\n");
        return 1.0;//���ݱ߳�ƽ���ıȹ�С�������ǰ��̭����ı���
    }
	double cosval = ((dx1*dx2+dy1*dy2)-sqrt((dx2*dx2+dy1*dy1)*(dx1*dx1+dy1*dy1)))/(2*sqrt((dx1*dy1*dx2*dy2)));
		//sqrt(dx1*dx2)/sqrt((dx1*dx1 + dy1*dy1)*(dx2*dx2 + dy2*dy2) + 1e-10);
    return cosval;
}

void findSquares( const Mat& gray0, vector<vector<Point> >& squares ) {// returns sequence of squares detected on the gray0. the sequence is stored in the specified memory storage
    squares.clear();
    Mat pyr,gray1,timg;
    Mat edge;
    // down-scale and upscale the gray0 to filter out the noise
    pyrDown(gray0, pyr, Size(gray0.cols/2, gray0.rows/2));
    pyrUp(pyr, timg, gray0.size());
    vector<vector<Point> > contours;
 
    // try several threshold levels
    for (int l = 0; l < 4; l++ ) {
        // hack: use Canny instead of zero threshold level.
        // Canny helps to catch squares with gradient shading
//      if (l == 0 ) {//�����Բ���l==0�����⴦���Ƿ����ڲ�Ӱ���ж������ε�ǰ���¼����жϹ���
//          // apply Canny. Take the upper threshold from slider
//          // and set the lower to 0 (which forces edges merging)
//          Canny(timg, gray1, 0, thresh, 5);
//          // dilate canny output to remove potential
//          // holes between edge segments
//          dilate(gray1, gray1, Mat(), Point(-1,-1));
//      } else {
            // apply threshold if l!=0:
            //     tgray(x,y) = gray1(x,y) < (l+1)*255/N ? 255 : 0
           gray1 = timg >= (l+1)*255/4;
//      }
 
		Mat gray2=gray1.clone();
		GaussianBlur(gray2,edge,Size(5,5),0,0);
		// ��4������Canny����  
		Canny( edge, edge, 3, 9,3 ); 
        // find contours and store them all as a list
        findContours(edge, contours, CV_RETR_LIST, CV_CHAIN_APPROX_SIMPLE);
		Mat result(edge.size(),CV_8U,Scalar(0)); 
		drawContours(result,contours,-1,Scalar(255),2); 
        imshow("��Ч��ͼ��Canny��Ե���2", result);   

        vector<Point> approx;
 
        // test each contour
        for (size_t i = 0; i < contours.size(); i++ ) {
            // approximate contour with accuracy proportional
            // to the contour perimeter
            approxPolyDP(Mat(contours[i]), approx, arcLength(Mat(contours[i]), true)*0.02, true);//0.02Ϊ��ë����ֱ��ϵ���������ë��������©�죬�����Ե���
 
            // square contours should have 4 vertices after approximation
            // relatively large area (to filter out noisy contours)
            // and be convex.
            // Note: absolute value of an area is used because
            // area may be positive or negative - in accordance with the
            // contour orientation
            if (approx.size() == 4 && isContourConvex(Mat(approx))) {
                double area;
                area=fabs(contourArea(Mat(approx)));
                if (1000.0<area) {//������������ڴ˷�Χ�ڡ���������������������С©�����������⣬�����˷�Χ��
//                  printf("area=%lg\n",area);
                    double maxCosine = 0.0;
 
                    // find the maximum cosine of the angle between joint edges
                    double cosine = fabs(angle(approx[2], approx[1], approx[0]));
                    maxCosine = MAX(maxCosine, cosine);
                    if (maxCosine==1.0) break;// //�߳��ȳ����趨��Χ
 
                    if (maxCosine < 0.1 ) {//�ĸ��Ǻ�ֱ����ȵ�������ɸ���ʵ���������������ԽСԽ�ϸ�
                        squares.push_back(approx);
                        return;//��⵽һ���ϸ�������ξͷ���
//                  } else {
//                      Log("Cosine>=0.1\n");
                    }
                }
            }
        }
   }
}

//�ҳ���������
int* findRectInfo(std::vector<cv::Point> rect)
{
    int rectInfo[4] = {0};
    int x[4]= {0},y[4]= {0};
    int maxX = 0,maxY = 0,minX = 2000,minY = 2000;
    //get the rect points
    for(int i=0;i<4;i++){
        x[i] = rect[i].x;
        y[i] = rect[i].y;
        if(maxX<x[i])
            maxX = x[i];
        if(maxY<y[i])
            maxY = y[i];
        if(minX>x[i])
            minX = x[i];
        if(minY>y[i])
            minY = y[i];
    }
    rectInfo[0] = minY;
    rectInfo[1] = minX;
    rectInfo[2] = maxY - minY;
    rectInfo[3] = maxX - minX;
    return rectInfo;
}

int _tmain(int argc, _TCHAR* argv[])
{
	//Mat src = imread("E:\\car.png", CV_BGR2GRAY);
	IplImage *img=cvLoadImage("E:\\ruihuapic.jpg");//ԭʼͼ  
	//IplImage *ximg = NULL;
	//CvSize czSize;              //Ŀ��ͼ��ߴ�
	////����Ŀ��ͼ���С  
 //   czSize.width = img->width * 0.5;  
 //   czSize.height = img->height * 0.5;
	//ximg = cvCreateImage(czSize, img->depth, img->nChannels);  
 //   cvResize(img, ximg, CV_INTER_AREA); 
    IplImage *grayimg=cvCreateImage(cvGetSize(img),8,1);//�Ҷ�ͼ  
    cvCvtColor(img,grayimg,CV_BGR2GRAY);//��ɫת�Ҷ�  
	//IplImage *grayimg=cvCreateImage(cvGetSize(img),8,1);//�Ҷ�ͼ  
    //cvCvtColor(img,grayimg,CV_BGR2GRAY);//��ɫת�Ҷ�
 //   cvThreshold(grayimg, grayimg,120, 225, CV_THRESH_BINARY);//��ֵ��  
	//cvShowImage("",grayimg);
	vector<vector<Point> > squares;
	findSquares(grayimg,squares);
	vector<int> compression_params;
    compression_params.push_back(CV_IMWRITE_PNG_COMPRESSION);
    compression_params.push_back(9);

    for(int i=0;i<squares.size();i++){
        int rect[4],*tmp;
        tmp = findRectInfo(squares[i]);
        for(int j=0;j<4;j++)
            rect[j] = *(tmp+j);
        Rect roi(rect[1],rect[0],rect[3],rect[2]);
		Mat src=cvLoadImage("E:\\car.png");//ԭʼͼ 
		Mat roi_of_image = src(roi);

        char * filename = new char[100];
        sprintf(filename,"H:\\pictest\\car.png");
        cv::imwrite(filename,roi_of_image,compression_params);
    }
	cvWaitKey(0);
	return 0;
}

