// OpencvTest.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"


//������ʵ�������ߴ���ͬ��ͼ���������Ĳ���  
//ʹ�ñ������صķ��� 
  
#include <iostream>  
#include <opencv2\core\core.hpp>  
#include <opencv2\highgui\highgui.hpp>  
#include "cv.h"  
using namespace std;  
using namespace cv;  
uchar toZero(uchar a);//���㺯����С������Ϊ0  
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
		CvSize size = cvSize(80, 270);//�����С  
		cvSetImageROI(pSrc, cvRect(109, 12, size.width, size.height));//����Դͼ��ROI  
		IplImage* pDest1 = cvCreateImage(size, pSrc->depth, pSrc->nChannels);//����Ŀ��ͼ��  
		cvCopy(pSrc, pDest1); //����ͼ��  
		cvResetImageROI(pDest1);//Դͼ����������ROI 
		CvSize czSize;              //Ŀ��ͼ��ߴ�
		//����Ŀ��ͼ���С  
		czSize.width = pDest1->width * 1;  
		czSize.height = pDest1->height * 1;
		IplImage *srcc = cvCreateImage(czSize, pDest1->depth, pDest1->nChannels);  
		cvResize(pDest1, srcc, CV_INTER_AREA); 
		cvSaveImage(imageDstName, srcc);//����Ŀ��ͼ�� 

	//IplImage *pSrc = cvLoadImage("H:\\pic\\subimagetest\\3.jpg", 1);  
 //   CvSize size = cvSize(600, 376);//�����С  
 //   cvSetImageROI(pSrc, cvRect(0, 0, size.width, size.height));//����Դͼ��ROI  
 //   IplImage* pDest1 = cvCreateImage(size, pSrc->depth, pSrc->nChannels);//����Ŀ��ͼ��  
 //   cvCopy(pSrc, pDest1); //����ͼ��  
 //   cvResetImageROI(pDest1);//Դͼ����������ROI  
 //   cvSaveImage("Image_1.jpg", pDest1);//����Ŀ��ͼ��

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
 //   imshow("ԭͼ1", imag1);  
 //   imshow("ԭͼ2", imag2);  
	//imshow("result1", result1); 
	//imwrite("E:\\1122\\sub\\3_4color.jpg",result1);  
	//GaussianBlur(result1, result1, Size(3, 3), 0, 0, BORDER_DEFAULT);
 //   cvtColor(result1, src_gray1, COLOR_BGR2GRAY);
	//// ��3������ʹ�� 3x3�ں�������  
	//blur( src_gray1, edge, Size(3,3) );  
 // 
	//// ��4������Canny����  
	//Canny( edge, edge, 3, 9,3 );  
 // 
	////��5����g_dstImage�ڵ�����Ԫ������Ϊ0   
	//dst = Scalar::all(0);  
 // 
	////��6��ʹ��Canny��������ı�Եͼg_cannyDetectedEdges��Ϊ���룬����ԭͼg_srcImage����Ŀ��ͼg_dstImage��  
	//result1.copyTo( dst, edge);  
 //       
	////��7����ʾЧ��ͼ   
	//imshow("��Ч��ͼ��Canny��Ե���2", dst);   
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