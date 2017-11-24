// cartest2.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"

using namespace cv;
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	Mat srcImage=imread("E:\\2.png");  
    Mat srcShowImage;  
    srcImage.copyTo(srcShowImage);  
    //imshow("a",srcImage);  
    int i,j;  
    int cPointB,cPointG,cPointR;  
    for(i=1;i<srcImage.rows;i++)  
        for(j=1;j<srcImage.cols;j++)  
        {  
            cPointB=srcImage.at<Vec3b>(i,j)[0];  
            cPointG=srcImage.at<Vec3b>(i,j)[1];  
            cPointR=srcImage.at<Vec3b>(i,j)[2];  
            if(cPointB>80&cPointR<80&cPointG<80)    //提取蓝色，将该区域设置为黑色  
            {  
                srcImage.at<Vec3b>(i,j)[0]=0;  
                srcImage.at<Vec3b>(i,j)[1]=0;  
                srcImage.at<Vec3b>(i,j)[2]=0;  
            }  
  
            else if(cPointB>200&cPointR>200&cPointG>200)  //提取白色，将其设置为黑色  
            {  
                srcImage.at<Vec3b>(i,j)[0]=0;  
                srcImage.at<Vec3b>(i,j)[1]=0;  
                srcImage.at<Vec3b>(i,j)[2]=0;  
            }  
  
            else  
            {  
                srcImage.at<Vec3b>(i,j)[0]=255;  
                srcImage.at<Vec3b>(i,j)[1]=255;  
                srcImage.at<Vec3b>(i,j)[2]=255;  
            }  
  
        }  
        cvtColor(srcImage,srcImage, CV_BGR2GRAY);    
        threshold(srcImage,srcImage,127, 255,CV_THRESH_BINARY);     
        //使用差分法，去掉不相关的区域。  
        for(i=1;i<srcImage.rows;i++)  
            for(j=1;j<srcImage.cols-1;j++)  
            {  
                srcImage.at<uchar>(i,j)=srcImage.at<uchar>(i,j+1)-srcImage.at<uchar>(i,j);  
  
            }  
  
            threshold(srcImage,srcImage,127, 255,CV_THRESH_BINARY_INV);//通过二值化的方式来取反。  
            //erode(srcImage,srcImage,Mat(5,5,CV_8U),Point(-1,-1),2);  //腐蚀  
            //  dilate(src,src,Mat(5,5,CV_8U),Point(-1,-1),2); //膨胀  
            //  morphologyEx(src,src,MORPH_OPEN,Mat(3,3,CV_8U),Point(-1,-1),1);   //开运算  
            //   morphologyEx(src,src,MORPH_CLOSE,Mat(3,3,CV_8U),Point(-1,-1),1);  //闭运算  
            erode(srcImage,srcImage,Mat(3,3,CV_8U),Point(-1,-1),5);  
            threshold(srcImage,srcImage,127,255,CV_THRESH_BINARY_INV);  
            imshow("a",srcImage);  
            vector<vector<Point> > contours;  
            vector<Vec4i> hierarchy;  
            findContours(srcImage, contours, hierarchy, CV_RETR_EXTERNAL, CV_CHAIN_APPROX_SIMPLE, Point(0, 0) );  
            Scalar color = Scalar( 0, 0, 0 );  
            for( int i = 0; i < contours.size(); i++ )  
            {    
                //使用边界框的方式    
                CvRect aRect =  boundingRect(contours[i]);  
                int tmparea=aRect.height*aRect.height;    
                if (((double)aRect.width/(double)aRect.height>2)&& ((double)aRect.width/(double)aRect.height<6)&& tmparea>=2000&&tmparea<=25000)    
                {    
                    rectangle(srcShowImage,cvPoint(aRect.x,aRect.y),cvPoint(aRect.x+aRect.width ,aRect.y+aRect.height),color,2);    
                    //cvDrawContours( dst, contours, color, color, -1, 1, 8 );    
                }    
            }   
			  
            imshow("da",srcShowImage); 
			cvWaitKey(0);
}

