// OpencvTest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"


//本程序实现两个尺寸相同的图像进行相减的操作  
//使用遍历像素的方法 
  
#include <iostream>  
#include <opencv2\core\core.hpp>  
#include <opencv2\highgui\highgui.hpp>  
#include "cv.h"  
using namespace std;  
using namespace cv;  
uchar toZero(uchar a);//置零函数，小于零则为0  
int main()  
{ 
	for(int n =1;n<101;n++)
	{
		char picFront1[50] = "H:\\2yellow\\1\\"; 
		char picEnd1[15] = ".jpg";
		char str1[4];
		sprintf(str1,"%d",n);
		string st1 = string(picFront1) + string(str1) + string(picEnd1);
		char imageName[50];
		strcpy(imageName, st1.c_str()); 
		char picFront2[50] = "E:\\1122\\sub\\stest\\"; 
		char picEnd2[13] = "_select.jpg";
		char str2[4];
		sprintf(str2,"%d",n);
		string st2 = string(picFront2) + string(str2)  + string(picEnd2);
		char imageDstName[50];
		strcpy(imageDstName, st2.c_str());
		IplImage *pSrc = cvLoadImage(imageName, 1);  
		CvSize size = cvSize(80, 270);//区域大小  
		cvSetImageROI(pSrc, cvRect(109, 12, size.width, size.height));//设置源图像ROI  
		IplImage* pDest1 = cvCreateImage(size, pSrc->depth, pSrc->nChannels);//创建目标图像  
		cvCopy(pSrc, pDest1); //复制图像  
		cvResetImageROI(pDest1);//源图像用完后，清空ROI 
		CvSize czSize;              //目标图像尺寸
		//计算目标图像大小  
		czSize.width = pDest1->width * 1;  
		czSize.height = pDest1->height * 1;
		IplImage *srcc = cvCreateImage(czSize, pDest1->depth, pDest1->nChannels);  
		cvResize(pDest1, srcc, CV_INTER_AREA); 
		cvSaveImage(imageDstName, srcc);//保存目标图像 

	//IplImage *pSrc = cvLoadImage("H:\\pic\\subimagetest\\3.jpg", 1);  
 //   CvSize size = cvSize(600, 376);//区域大小  
 //   cvSetImageROI(pSrc, cvRect(0, 0, size.width, size.height));//设置源图像ROI  
 //   IplImage* pDest1 = cvCreateImage(size, pSrc->depth, pSrc->nChannels);//创建目标图像  
 //   cvCopy(pSrc, pDest1); //复制图像  
 //   cvResetImageROI(pDest1);//源图像用完后，清空ROI  
 //   cvSaveImage("Image_1.jpg", pDest1);//保存目标图像

 //   Mat imag1, imag2, result1,src_gray1,dst,edge;  
 //   imag1 = imread("H:\\pic\\subimagetest\\3.jpg");  
 //   imag2 = imread("H:\\pic\\subimagetest\\4.jpg");  
	//imwrite("E:\\1122\\sub\\3.jpg",imag1);
	//imwrite("E:\\1122\\sub\\4.jpg",imag2);
 //   result1 = imag1.clone();  
 //     
 //   int rowNumber = result1.rows;  
 //   int colNumber = result1.cols;  
 //   for (int i = 0; i < rowNumber; i++)  
 //   {  
 //       for (int j = 0; j < colNumber; j++)  
 //       {  
 //           result1.at<Vec3b>(i, j)[0] = toZero(imag1.at<Vec3b>(i, j)[0] - imag2.at<Vec3b>(i, j)[0]);  
 //           result1.at<Vec3b>(i, j)[1] = toZero(imag1.at<Vec3b>(i, j)[1] - imag2.at<Vec3b>(i, j)[1]);  
 //           result1.at<Vec3b>(i, j)[2] = toZero(imag1.at<Vec3b>(i, j)[2] - imag2.at<Vec3b>(i, j)[2]);  
 //       }  
 //   }  
 //
 //   imshow("原图1", imag1);  
 //   imshow("原图2", imag2);  
	//imshow("result1", result1); 
	//imwrite("E:\\1122\\sub\\3_4color.jpg",result1);  
	//GaussianBlur(result1, result1, Size(3, 3), 0, 0, BORDER_DEFAULT);
 //   cvtColor(result1, src_gray1, COLOR_BGR2GRAY);
	//// 【3】先用使用 3x3内核来降噪  
	//blur( src_gray1, edge, Size(3,3) );  
 // 
	//// 【4】运行Canny算子  
	//Canny( edge, edge, 3, 9,3 );  
 // 
	////【5】将g_dstImage内的所有元素设置为0   
	//dst = Scalar::all(0);  
 // 
	////【6】使用Canny算子输出的边缘图g_cannyDetectedEdges作为掩码，来将原图g_srcImage拷到目标图g_dstImage中  
	//result1.copyTo( dst, edge);  
 //       
	////【7】显示效果图   
	//imshow("【效果图】Canny边缘检测2", dst);   
 //   imshow("src_gray1", src_gray1); 
	//imwrite("E:\\1122\\sub\\3_4gray.jpg",src_gray1);  
	}
    waitKey();  
    return 0;  
}  
  
uchar toZero(uchar a)  
{  
    if (a < 0)  
        return 0;  
    else  
        return a;  
}  