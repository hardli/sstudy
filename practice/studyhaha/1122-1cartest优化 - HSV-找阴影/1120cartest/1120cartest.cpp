// 1120cartest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <sstream>
#include <cstdio>
#include <cstring>
#include <iostream>
#include<cv.h>
#include <cmath>
#pragma comment(lib,"opencv_highgui249d.lib") 
#include <opencv2/opencv.hpp>  
#include<opencv2/highgui/highgui.hpp>  
#include<opencv2/imgproc/imgproc.hpp> 
 
using namespace cv;  
  
class Image
{
public:
    Image(void);
    ~Image(void);
    static void cloneImage(IplImage *src,IplImage*dst);
    static void ZerosImage(IplImage * src);
    static void dilateImage(IplImage* src, IplImage *dst);
    static void erodeImage(IplImage* src,IplImage*dst);
    static void rgb2gray(IplImage* src, IplImage* dst);
    static void gray2bw(IplImage* src, IplImage* dst,unsigned char value);
};

using namespace std;

//RGB转HSV
void RGBtoHSV( float r, float g, float b, float *h, float *s, float *v )
{
    float min, max, delta;
    min = MIN( r, MIN( g, b ));
    max = MAX( r, MAX( g, b ));
    *v = max/255.0;               // v
    delta = max - min;
    if( max != 0 )
        *s = delta / max;       // s
    else {
        // r = g = b = 0        // s = 0, v is undefined
        *s = 0;
        *h = -1;
        return;
    }
    if( r == max )
        *h = ( g - b ) / delta;     // between yellow & magenta
    else if( g == max )
        *h = 2 + ( b - r ) / delta; // between cyan & yellow
    else
        *h = 4 + ( r - g ) / delta; // between magenta & cyan
    *h *= 60;               // degrees
    if( *h < 0 )
        *h += 360;
}
int main(){
    //char imageName[60] = "E:\\软件下载和接收资料\\lpr第三版\\第三版\\5.jpg";
	for(int n =1; n <101; n++)
	{
		//char picFront1[50] = "H:\\2yellow\\1\\"; 
		char picFront1[50] = "E:\\1122\\sub\\stest\\"; 
		char picEnd1[15] = "_select.jpg";
		char str1[4];
		sprintf(str1,"%d",n);
		string st1 = string(picFront1) + string(str1) + string(picEnd1);
		char imageName[50];
		strcpy(imageName, st1.c_str());
		//char imageName[60] = "H:\\2yellow\\2result\\";
		//char picFront2[50] = "E:\\pictest\\黄牌\\优化测试\\"; 
		char picFront2[50] = "E:\\1122\\sub\\sresult\\"; 
		char picEnd2[13] = "_result.jpg";
		char str2[4];
		sprintf(str2,"%d",n);
		string st2 = string(picFront2) + string(str2)  + string(picEnd2);
		char imageDstName[50];
		strcpy(imageDstName, st2.c_str());

		char picFront3[20] = "E:\\1122\\test\\"; 
		char picEnd3[8] = "_bw.jpg";
		char str3[4];
		sprintf(str3,"%d",n);
		string st3 = string(picFront3) + string(str3) + string(picEnd3);
		char imageBwName[50];
		strcpy(imageBwName, st3.c_str());

		//char imageDstName[43] = "E:\\pictest\\黄牌\\1.jpg";
		//char imageBwName[42]  = "E:\\pictest\\黄牌\\1.jpg";
		IplImage* srcImage = NULL,*image = NULL,*bwImage = NULL;
    
		//cvNamedWindow("srcImage",1);
		//cvNamedWindow("bwImage",1);
		//cvShowImage("srcImage",srcImage);
		int imageWidth,imageHeight;  
		int maxDif = 47; 
		//找到蓝色区域
		int i= 0,j = 0;
		unsigned char * pPixel = NULL;
		unsigned char   pixelR = 0,pixelG = 0,pixelB = 0;
	    float pixelH[1],pixelS[1],pixelV[1];
		unsigned char R = 28,G = 63, B = 138; 
		unsigned char R1 = 95,G1 = 93,B1 = 32; 
		unsigned char R2 = 93,G2 = 108, B2 = 32;
		float iLowH =28;
	    float iHighH = 72;
		float iHighV =0.2;
	    float iHighHH = 200;
		double length,area,rectArea;
		double rectDegree = 0.0; //矩形度
		double long2Short = 1.0; //体态比
		//计算边界序列的参数 长度 面积 矩形 最小矩形 
		//并输出每个边界的参数
		CvRect rect;
		CvBox2D box;
		int imageCnt = 1;
		double axisLong = 0.0, axisShort = 0.0;
		double temp;

		Mat imgg = imread(imageName)*5;
		char picFrontt1[50] = "E:\\1122\\"; 
		string stt1 = string(picFrontt1) + string(str1) + string(picEnd1);
		char imageNamee[50];
		strcpy(imageNamee, stt1.c_str());
		imwrite(imageNamee,imgg);
		while ((srcImage = cvLoadImage(imageName,1)) != NULL)
		{
			//cvShowImage("srcImage",srcImage); 
			cout<<imageName<<": "<<endl;
			imageWidth = srcImage->width;
			imageHeight = srcImage->height;
			image = cvCreateImage(cvSize(imageWidth,imageHeight),8,3);
			//image = cvCloneImage(srcImage);
			Image::cloneImage(srcImage,image);

			bwImage = cvCreateImage(cvGetSize(srcImage),srcImage->depth,1);
			//cvZero(bwImage);
			Image::ZerosImage(bwImage);

			//cvCvtColor(srcImage,bwImage,CV_BGR2HSV);
			for (i = 0; i< imageHeight;i++)
			{
				for (j = 0;j<imageWidth;j++)
				{
					pPixel = (unsigned char*)srcImage->imageData + i*srcImage->widthStep + j*3;
					pixelB = pPixel[0];
					pixelG = pPixel[1];
					pixelR = pPixel[2];
					RGBtoHSV(pixelR,pixelG,pixelB,pixelH,pixelS,pixelV);
					/*if (pixelH[0] > iLowH && pixelH[0] < iHighH)
					{
						*((unsigned char*)bwImage->imageData + i*bwImage->widthStep + j) = 255;
					}
					else {
						*((unsigned char*)bwImage->imageData + i*bwImage->widthStep + j) = 0;
					}*/
					if (pixelH[0] > iHighHH && pixelV[0] > 0 && pixelV[0]< iHighV)
					{
						*((unsigned char*)bwImage->imageData + i*bwImage->widthStep + j) = 255;
					}
					else {
						*((unsigned char*)bwImage->imageData + i*bwImage->widthStep + j) = 0;
					}

					/*if (abs(pixelB - B2) < maxDif && abs(pixelG - G2)< maxDif && abs(pixelR - R2)< maxDif)
					{
						*((unsigned char*)bwImage->imageData + i*bwImage->widthStep + j) = 255;
					}
					else {
						*((unsigned char*)bwImage->imageData + i*bwImage->widthStep + j) = 0;
					}*/
					
				}
			}
			//cvShowImage("bwImage",bwImage);
			//cvSaveImage(imageBwName,bwImage);
			 //cvWaitKey(0);
			//膨胀
			//cvDilate(bwImage,bwImage,0,3);
			Image::dilateImage(bwImage,bwImage);
			Image::dilateImage(bwImage,bwImage);
			Image::dilateImage(bwImage,bwImage);

			//cvErode (bwImage,bwImage,0,3);
			Image::erodeImage(bwImage,bwImage);
			Image::erodeImage(bwImage,bwImage);
			Image::erodeImage(bwImage,bwImage);

			//cvShowImage("bwImage",bwImage);
			//cvWaitKey(0);

			//新图，将轮廓绘制到dst
			IplImage *dst = cvCreateImage(cvGetSize(srcImage),8,3); 
			//dst = cvCloneImage(srcImage);//赋值为0
			Image::cloneImage(srcImage,dst);
			//寻找轮廓
			CvMemStorage *storage = cvCreateMemStorage(0);
			CvSeq * seq = cvCreateSeq(CV_SEQ_ELTYPE_POINT, sizeof(CvSeq), sizeof(CvPoint), storage);
			CvSeq * tempSeq = cvCreateSeq(CV_SEQ_ELTYPE_POINT, sizeof(CvSeq), sizeof(CvPoint), storage);
			int cnt = cvFindContours(bwImage,storage,&seq);//返回轮廓的数目
			cout<<"number of contours   "<<cnt<<endl;
			//cvShowImage("bwImage",bwImage);    //难道使用cvFindContours会改变源图像？需要实现保存一下

			for (tempSeq = seq;tempSeq != NULL; tempSeq = tempSeq->h_next)
			{
		    
				length = cvArcLength(tempSeq);
				area =  cvContourArea(tempSeq);
				//筛选面积比较大的区域
				if (area > 50 && area < 50000)
				{
                
					//cout<<"Points:  "<<tempSeq->total<<endl;
					//外接矩形
					rect = cvBoundingRect(tempSeq,1);
					//绘制轮廓和外接矩形
					//cvDrawContours(dst,tempSeq,CV_RGB(255,0,0),CV_RGB(255,0,0),0);
					//cvRectangleR(dst,rect,CV_RGB(0,255,0));
					//cvShowImage("dst",dst);

					//绘制外接最小矩形
					CvPoint2D32f pt[4];
					box = cvMinAreaRect2(tempSeq,0);
					cvBoxPoints(box,pt);
					//下面开始分析图形的形状特征 
					//长轴 短轴
					axisLong = sqrt(pow(pt[1].x -pt[0].x,2) + pow(pt[1].y -pt[0].y,2));
					axisShort = sqrt(pow(pt[2].x -pt[1].x,2) + pow(pt[2].y -pt[1].y,2));

					if (axisShort > axisLong)
					{  
						temp = axisLong;
						axisLong = axisShort;
						axisShort= temp;
					}  
					rectArea = axisLong*axisShort; 
					rectDegree =  area/rectArea;  
					//体态比or长宽比 最下外接矩形的长轴和短轴的比值
					long2Short = axisLong/axisShort;
             
					if (long2Short> 1.5 && long2Short < 3.8 && rectDegree > 0.63 && rectArea > 50 && rectArea <1000)
					{
						cout<<"Length:  "<<length<<endl;
						cout<<"Area  :  "<<area<<endl;
						cout<<"long axis ："<<axisLong<<endl;
						cout<<"short axis: "<<axisShort<<endl;
						cout<<"long2Short: "<<long2Short<<endl;
						cout<<"rectArea:  "<<rectArea<<endl;
						cout<<"rectDegree:  "<<rectDegree<<endl;
						
						for(int i = 0;i<4;++i){
							cvLine(dst,cvPointFrom32f(pt[i]),cvPointFrom32f(pt[((i+1)%4)?(i+1):0]),CV_RGB(8,105,24),2);
						}
					}
					//cvShowImage("dst",dst);
					//cvWaitKey();
				}
			}
			//cvShowImage("dst",dst);
			cvSaveImage(imageDstName,dst);
			//cvWaitKey(0);

			imageCnt++;
			sprintf(imageName,"%d.jpg",imageCnt);
			sprintf(imageBwName,"%d_bw.jpg",imageCnt);
			sprintf(imageDstName,"%d_dst.jpg",imageCnt);
			cout<<"\n\n";
		}
	}
	cvWaitKey();
    return 0;
}

 