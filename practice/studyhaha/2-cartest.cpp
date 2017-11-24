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
		int counttt =0;
		int a[2];
		//char picFront1[50] = "H:\\2yellow\\1\\"; 
		char picFront1[50] = "E:\\1122\\sub\\sresult\\"; 
		char picEnd1[15] = "_result.jpg";
		char str1[4];
		sprintf(str1,"%d",n);
		string st1 = string(picFront1) + string(str1) + string(picEnd1);
		char imageName[50];
		strcpy(imageName, st1.c_str());
		//char imageName[60] = "E:\\1122\\sub\\stest\\1_select.jpg";
		//char picFront2[50] = "E:\\pictest\\黄牌\\优化测试\\"; 
		char picFront2[50] = "E:\\1122\\sub\\sresult\\ssresult\\"; 
		char picEnd2[20] = "_result_s.jpg";
		char str2[4];
		sprintf(str2,"%d",n);
		string st2 = string(picFront2) + string(str2)  + string(picEnd2);
		char imageDstName[50];
		strcpy(imageDstName, st2.c_str());

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
		
		float iHighV =0.15;
	    float iHighHH = 200;

		/*Mat imgg = imread(imageName)*5;
		char picFrontt1[50] = "E:\\1122\\"; 
		string stt1 = string(picFrontt1) + string(str1) + string(picEnd1);
		char imageNamee[50];
		strcpy(imageNamee, stt1.c_str());
		imwrite(imageNamee,imgg);*/
		if ((srcImage = cvLoadImage(imageName,1)) != NULL)
		{
			imageWidth = srcImage->width;
			imageHeight = srcImage->height;
			//for (i = 0; i< imageHeight;i++)
			for (i = imageHeight-1; i>0;i--)
			{
				for (j = imageWidth-1;j>0;j--)
				{
					pPixel = (unsigned char*)srcImage->imageData + i*srcImage->widthStep + j*3;
					pixelB = pPixel[0];
					pixelG = pPixel[1];
					pixelR = pPixel[2];
					RGBtoHSV(pixelR,pixelG,pixelB,pixelH,pixelS,pixelV);
					//if (pixelH[0] > iHighHH && pixelV[0] > 0 && pixelV[0]< iHighV)
					if (pixelV[0] > iHighV && pixelS[0] > 0.6)
					{
						counttt++;
						if(counttt == 1)
						{
						 a[0] = i;
						 a[1] = j;
						}
					}
					else {
						//*((unsigned char*)bwImage->imageData + i*bwImage->widthStep + j) = 0;
					}
					
				}
			}

			char picFront11[50] = "H:\\2yellow\\1\\"; 
		    char picEnd11[15] = ".jpg";
			char str11[4];
			sprintf(str11,"%d",n);
			string st11 = string(picFront11) + string(str11) + string(picEnd11);
			char imageNamee[50];
			strcpy(imageNamee, st11.c_str()); 
			bwImage = cvLoadImage(imageNamee,1);
			CvSize size = cvSize(30, 20);//区域大小
			Scalar color = Scalar( 0, 255, 0 ); 
			rectangle((Mat)bwImage,cvPoint(0+109, a[0]-20+137),cvPoint(109+size.width ,a[0]+137),color,2);
			cout<<imageName<<"\n\n";
			//cvShowImage("srcImage",srcImage);
			cvSaveImage(imageDstName, bwImage);//保存目标图像  
			//cvSetImageROI(srcImage, cvRect(0, a[0]-120, size.width, size.height));//设置源图像ROI  
			//IplImage* pDest1 = cvCreateImage(size, srcImage->depth, srcImage->nChannels);//创建目标图像  
			//cvCopy(srcImage, pDest1); //复制图像  
			//cvResetImageROI(pDest1);//源图像用完后，清空ROI 
			////namedWindow("pDest1", CV_WINDOW_NORMAL);
			////cvShowImage("pDest1",pDest1);
			//cout<<imageDstName<<"\n\n";
			//cvSaveImage(imageDstName, pDest1);//保存目标图像 

			
		}
	}
	cvWaitKey();
    return 0;
}

 