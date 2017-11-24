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
		int counttt =0;
		int counttt1 = 0;
		int a[2];
		int b[2];
		char picFront1[50] = "H:\\SAMPLE\\10\\"; 
		//char picFront1[50] = "E:\\1122\\sub\\stest\\"; 
		char picEnd1[15] = ".jpg";
		char str1[4];
		sprintf(str1,"%d",n);
		string st1 = string(picFront1) + string(str1) + string(picEnd1);
		char imageName[50];
		strcpy(imageName, st1.c_str());
		//char imageName[60] = "E:\\1122\\sub\\stest\\1_select.jpg";
		//char picFront2[50] = "E:\\pictest\\黄牌\\优化测试\\"; 
		char picFront2[50] = "E:\\1124test\\10_result\\"; 
		char picEnd2[20] = "_result.jpg";
		char str2[4];
		sprintf(str2,"%d",n);
		string st2 = string(picFront2) + string(str2)  + string(picEnd2);
		char imageDstName[50];
		strcpy(imageDstName, st2.c_str());
		
		char picFront3[50] = "E:\\1124test\\10\\"; 
		char picEnd3[20] = ".jpg";
		char str3[4];
		sprintf(str3,"%d",n);
		string st3 = string(picFront3) + string(str3)  + string(picEnd3);
		char imageDstName3[50];
		strcpy(imageDstName3, st3.c_str());

		IplImage* srcImage = NULL,*image = NULL,*bwImage = NULL;
    
		int imageWidth,imageHeight;  
		int maxDif = 47; 
		//找到蓝色区域
		int i= 0,j = 0;
		int m= 0,k = 0;
		unsigned char * pPixel = NULL;
		unsigned char * pPixel1 = NULL;
		unsigned char   pixelR = 0,pixelG = 0,pixelB = 0;
		unsigned char   pixelR1 = 0,pixelG1 = 0,pixelB1 = 0;
	    float pixelH[1],pixelS[1],pixelV[1];
		 float pixelH1[1],pixelS1[1],pixelV1[1];
		float iHighV =0.15;
	    float iHighHH = 200;

		int cutX1 = 109,cutY1 = 12, cutWide1=80,cutHeight1=270;
		if ((srcImage = cvLoadImage(imageName,1)) != NULL)
		{
			cvSaveImage(imageDstName3, srcImage);//保存目标图像 
			for (i = cutY1+cutHeight1-1; i>cutY1;i--)
			{
				for (j = cutX1+cutWide1-1;j>cutX1;j--)
				{
					pPixel = (unsigned char*)srcImage->imageData + i*srcImage->widthStep + j*3;
					pixelB = pPixel[0];
					pixelG = pPixel[1];
					pixelR = pPixel[2];
					RGBtoHSV(pixelR,pixelG,pixelB,pixelH,pixelS,pixelV);
					if (pixelH[0] > iHighHH && pixelV[0] > 0 && pixelV[0]< iHighV)
					{
						counttt++;
						if(counttt == 1)
						{
							a[0] = i;
							a[1] = j;
							int cutX2 = cutX1,cutY2 = a[0]-90, cutWide2=40,cutHeight2=90;
							for (m = cutY2+cutHeight2-1; m>cutY2;m--)
							{
								for (k = cutX2+cutWide1-2;k>cutX2;k--)
								{
									pPixel1 = (unsigned char*)srcImage->imageData + m*srcImage->widthStep + k*3;
									pixelB1 = pPixel1[0];
									pixelG1 = pPixel1[1];
									pixelR1 = pPixel1[2];
									RGBtoHSV(pixelR1,pixelG1,pixelB1,pixelH1,pixelS1,pixelV1);
									if (pixelV1[0] > iHighV && pixelS1[0] > 0.6)
									{
										counttt1++;
										if(counttt1 == 1)
										{
											b[0] = m;
											b[1] = k;
										}
									}
									else {
									}
					
								}
							}
						}
					}
					else {
					}
					
				}
			}
			CvSize size = cvSize(30, 20);//区域大小
			Scalar color = Scalar( 0, 255, 0 ); 
			rectangle((Mat)srcImage,cvPoint(cutX1, b[0]-20),cvPoint(cutX1+size.width ,b[0]),color,2);
			cout<<imageName<<"\n\n";
			//cvShowImage("srcImage",srcImage);
			cvSaveImage(imageDstName, srcImage);//保存目标图像 

			
		}
	}
	cvWaitKey();
    return 0;
}

 