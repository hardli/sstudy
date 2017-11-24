// ruihuaTest2.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include <iostream>
#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include <cv.h>  
#include<highgui.h>
using namespace cv;
using namespace std;

//�ֶ�ʵ��������˹����ͼ����  
void sharpenImage1(const Mat &image, Mat &result)  
{  
    result.create(image.size(),image.type());//Ϊ���ͼ���������  
    /*������˹�˲���3*3 
         0  -1   0 
        -1   5  -1 
         0  -1   0  */  
    //���������ΧһȦ�����������ֵ  
    for(int i=1; i<image.rows-1; i++)  
    {  
        const uchar * pre = image.ptr<const uchar>(i-1);//ǰһ��  
        const uchar * cur = image.ptr<const uchar>(i);//��ǰ�У���i��  
        const uchar * next = image.ptr<const uchar>(i+1);//��һ��  
        uchar * output = result.ptr<uchar>(i);//���ͼ��ĵ�i��  
        int ch = image.channels();//ͨ������  
        int startCol = ch;//ÿһ�еĿ�ʼ�����  
        int endCol = (image.cols-1)* ch;//ÿһ�еĴ��������  
        for(int j=startCol; j < endCol; j++)  
        {  
            //���ͼ��ı���ָ���뵱ǰ�е�ָ��ͬ������, ��ÿ�е�ÿһ�����ص��ÿһ��ͨ��ֵΪһ��������, ��ΪҪ���ǵ�ͼ���ͨ����  
            //saturate_cast<uchar>��֤�����uchar��Χ��  
            *output++ = saturate_cast<uchar>(5*cur[j]-pre[j]-next[j]-cur[j-ch]-cur[j+ch]);  
        }  
    }  
    //������ΧһȦ������ֵ��Ϊ0  
    result.row(0).setTo(Scalar(0));  
    result.row(result.rows-1).setTo(Scalar(0));  
    result.col(0).setTo(Scalar(0));  
    result.col(result.cols-1).setTo(Scalar(0));  
    /*/����Ҳ���Գ��Խ�����ΧһȦ����Ϊԭͼ������ֵ 
    image.row(0).copyTo(result.row(0)); 
    image.row(image.rows-1).copyTo(result.row(result.rows-1)); 
    image.col(0).copyTo(result.col(0)); 
    image.col(image.cols-1).copyTo(result.col(result.cols-1));*/  
}  
  
//����OpenCV����ʵ��������˹����ͼ����  
void sharpenImage2(const Mat &image, Mat &result)  
{  
    Mat kernel = (Mat_<float>(3,3) << 0,-1,0,-1,5,-1,0,-1,0);  
    filter2D(image,result,image.depth(),kernel);  
}  
  
//main����  
void main(){      
    Mat mat = imread("H:\\pic\\034_2014-07-04 13-12-53_35.jpg");  
	resize(mat,mat,Size(mat.cols/2,mat.rows/2),0,0,INTER_LINEAR); 
	
    Mat result1;  
    Mat result2;  
  
    sharpenImage1(mat,result1);  
    sharpenImage2(mat,result2);  
  
    namedWindow("src");  
    namedWindow("result1");  
    namedWindow("result2");  
    imshow("src",mat);  
    imshow("result1",result1);  
    imshow("result2",result2);  
	imwrite("E:\\pictest\\ruihuaLPLS.jpg",result2);
	waitKey();
} 

