using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    class Program
    {
        //结构体
        #region
        [StructLayout(LayoutKind.Sequential)]
        public struct DetectParamter
        {
            [MarshalAs(UnmanagedType.I4)]
            public int HammingFilter_N;
            [MarshalAs(UnmanagedType.R4)]
            public float Hamming_Band;
            [MarshalAs(UnmanagedType.I4)]
            public int SRCFilter_N;
            [MarshalAs(UnmanagedType.I4)]
            public int Summit_Width;
            [MarshalAs(UnmanagedType.R4)]
            public float Summit_Begin;
            [MarshalAs(UnmanagedType.R4)]
            public float Summit_End;
            [MarshalAs(UnmanagedType.R4)]
            public float Summit_HeightUp;
            [MarshalAs(UnmanagedType.R4)]
            public float Summit_RealHeightUp;
            [MarshalAs(UnmanagedType.R4)]
            public float Summit_HeightDown;
            [MarshalAs(UnmanagedType.R4)]
            public float Summit_RealHeightDown;
            [MarshalAs(UnmanagedType.R4)]
            public float Summit_SynLimit;
            [MarshalAs(UnmanagedType.I1)]
            public bool isNegtivesummit;
            [MarshalAs(UnmanagedType.I1)]
            public bool NegtivePermit;
            [MarshalAs(UnmanagedType.I4)]
            public int databeg;
            [MarshalAs(UnmanagedType.I4)]
            public int dataend;
            [MarshalAs(UnmanagedType.I1)]
            public bool Reserve1;    //备用字段1
            [MarshalAs(UnmanagedType.I1)]
            public bool Reserve2;    //备用字段2
            [MarshalAs(UnmanagedType.I4)]
            public int Reserve3;     //备用字段3
            [MarshalAs(UnmanagedType.I4)]
            public int Reserve4;     //备用字段4
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve5;   //备用字段5
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve6;   //备用字段6
        };

        //峰信息结构体
        [StructLayout(LayoutKind.Sequential)]
        public struct SummitForDll
        {
            [MarshalAs(UnmanagedType.I4)]
            public int Begin_Place;       //谷谷切割起始位置
            [MarshalAs(UnmanagedType.I4)]
            public int Vertical_beg;      //垂直切割起始位置
            [MarshalAs(UnmanagedType.I4)]
            public int Vertical_end;      //垂直切割截止位置
            [MarshalAs(UnmanagedType.I4)]
            public int Max_Place;         //峰的极值点位置
            [MarshalAs(UnmanagedType.I4)]
            public int End_Place;         //谷谷切割截止位置
            [MarshalAs(UnmanagedType.I4)]
            public int HalfWidth;         //谷谷切割半峰宽
            [MarshalAs(UnmanagedType.I4)]
            public int Vertical_Halfwidth;//垂直切割半峰宽
            [MarshalAs(UnmanagedType.R4)]
            public float Area;           //谷谷切割面积
            [MarshalAs(UnmanagedType.R4)]
            public float VerticalArea;   //垂直切割锋面积
            [MarshalAs(UnmanagedType.R4)]
            public float Vale_Height;    //谷谷切割高度
            [MarshalAs(UnmanagedType.R4)]
            public float Vertical_Height;//峰的垂直切割高度
            [MarshalAs(UnmanagedType.I1)]
            public bool IsPeak;           //是否正峰
            [MarshalAs(UnmanagedType.I4)]
            public int zeroplace;         //峰顶点对应的零点位置
            [MarshalAs(UnmanagedType.R4)]
            public float Begin_ModifyHeight;   //谷谷切割开始时的修正高度
            [MarshalAs(UnmanagedType.R4)]
            public float End_ModifyHeight;     //谷谷切割结束时的修正高度
            [MarshalAs(UnmanagedType.R4)]
            public float ping9Width;  //谷谷切割9/10峰宽
        };

        //峰Head结构体
        [StructLayout(LayoutKind.Sequential)]
        public struct FYP2GraphHead
        {
            [MarshalAs(UnmanagedType.R4)]
            public float Version;      //规范版本号 从1.000计起
            [MarshalAs(UnmanagedType.U1)]
            public byte TypeID;        //区分谱图类型 1:标样 2:样品
            [MarshalAs(UnmanagedType.I8)]
            public Int64 CreateTime;     //谱图生成时间 格式为YYYYMMDDhhmmss，例如20180813151010
            [MarshalAs(UnmanagedType.U1)]
            public byte IsAnalyse;      //文件标识位 未分析：0 已分析：1
            [MarshalAs(UnmanagedType.R4)]
            public float DataInterval;   //数据点间隔时间t 单位ms
            [MarshalAs(UnmanagedType.U1)]
            public byte DataUnit;       //数据点单位 0表示毫伏(mV),1表示微伏(μV)
            [MarshalAs(UnmanagedType.U1)]
            public byte ChannelCount;   //通道数量m
            [MarshalAs(UnmanagedType.I4)]
            public int Reserve1;       //备用字段1
            [MarshalAs(UnmanagedType.I4)]
            public int Reserve2;       //备用字段2
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve3;       //备用字段3
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve4;       //备用字段4
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve5;       //备用字段5
        };

        //通道信息
        [StructLayout(LayoutKind.Sequential)]
        public struct FYP2ChannelInfo
        {
            [MarshalAs(UnmanagedType.I2)]
            public Int16 UseData;        //使用的数据区号 原始数据区:0 修正1数据区:1 修正2数据区:2
            [MarshalAs(UnmanagedType.I2)]
            public Int16 ApexNum;        //通道中峰的个数n
            [MarshalAs(UnmanagedType.I4)]
            public int DataNum0;        //数据点数k0
            [MarshalAs(UnmanagedType.R4)]
            public float MaxData0;        //最大值
            [MarshalAs(UnmanagedType.R4)]
            public float MinData0;        //最小值
            [MarshalAs(UnmanagedType.I4)]
            public int DataNum1;        //数据点数k1
            [MarshalAs(UnmanagedType.R4)]
            public float MaxData1;        //最大值
            [MarshalAs(UnmanagedType.R4)]
            public float MinData1;        //最小值
            [MarshalAs(UnmanagedType.I4)]
            public int DataNum2;        //数据点数k2
            [MarshalAs(UnmanagedType.R4)]
            public float MaxData2;        //最大值
            [MarshalAs(UnmanagedType.R4)]
            public float MinData2;        //最小值
            [MarshalAs(UnmanagedType.I4)]
            public int Reserve1;       //备用字段1
            [MarshalAs(UnmanagedType.I4)]
            public int Reserve2;       //备用字段2
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve3;       //备用字段3
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve4;       //备用字段4
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve5;       //备用字段5
        };

        //峰信息
        [StructLayout(LayoutKind.Sequential)]
        struct FYP2PeakInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string elementName;
            [MarshalAs(UnmanagedType.I2)]
            public Int16 ApexIndex;      //峰序号 从0开始
            [MarshalAs(UnmanagedType.I4)]
            public int StartIndex;     //峰的开始点数
            [MarshalAs(UnmanagedType.I4)]
            public int EndIndex;       //峰的结束点序号
            [MarshalAs(UnmanagedType.R4)]
            public float StartVal;       //开始时的电压
            [MarshalAs(UnmanagedType.R4)]
            public float EndVal;         //结束时的电压
            [MarshalAs(UnmanagedType.I4)]
            public int ApexTimeIndex;  //峰的数据点序号
            [MarshalAs(UnmanagedType.R4)]
            public float ApexHigh;       //峰的电压值
            [MarshalAs(UnmanagedType.R4)]
            public float RelativeHigth;  //相对高
            [MarshalAs(UnmanagedType.R4)]
            public float HalfWidth;      //半峰宽
            [MarshalAs(UnmanagedType.R4)]
            public float Acreage;        //峰面积
            [MarshalAs(UnmanagedType.R4)]
            public float BaseSkew;       //基线斜率
            [MarshalAs(UnmanagedType.I4)]
            public int GgqgStartIndex; //谷谷切割起点
            [MarshalAs(UnmanagedType.I4)]
            public int GgqgEndIndex;   //谷谷切割终点
            [MarshalAs(UnmanagedType.R4)]
            public float GgqgHigth;      //谷谷切割峰高
            [MarshalAs(UnmanagedType.R4)]
            public float GgqgAcreage;    //谷谷切割峰面积
            [MarshalAs(UnmanagedType.R4)]
            public float GgqgWidth;      //谷谷切割峰宽
            [MarshalAs(UnmanagedType.R4)]
            public float GgqgHaltWidth;  //谷谷切割半峰宽
            [MarshalAs(UnmanagedType.R4)]
            public float GgqgZzd;        //谷谷切割最值点
            [MarshalAs(UnmanagedType.I4)]
            public int CzqgStartIndex; //垂直切割起点
            [MarshalAs(UnmanagedType.I4)]
            public int CzqgEndIndex;   //垂直切割终点
            [MarshalAs(UnmanagedType.R4)]
            public float CzqgHigth;      //垂直切割峰高
            [MarshalAs(UnmanagedType.R4)]
            public float CzqgAcreage;    //垂直切割峰面积
            [MarshalAs(UnmanagedType.I4)]
            public int Reserve1;       //备用字段1
            [MarshalAs(UnmanagedType.I4)]
            public int Reserve2;       //备用字段2
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve3;       //备用字段3
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve4;       //备用字段4
            [MarshalAs(UnmanagedType.R4)]
            public float Reserve5;       //备用字段5
        };

        [StructLayout(LayoutKind.Sequential)]
        struct SElementBlyzInfo2
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string elementName;
            [MarshalAs(UnmanagedType.I2)]
            public short elementtype;    // 组分类型
            [MarshalAs(UnmanagedType.I4)]
            public int channelno;        //通道号
            [MarshalAs(UnmanagedType.R4)]
            public float blsj;           //保留时间
        };

        //特殊处理条件
        [StructLayout(LayoutKind.Sequential)]
        struct SpecialTreatCond
        {
            [MarshalAs(UnmanagedType.I4)]
            public int channelNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 60, ArraySubType = UnmanagedType.R4)]
            public float[] times;
            [MarshalAs(UnmanagedType.I4)]
            public int timeNum;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string elementName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 120)]
            public string typeNoParam;
        };
        #endregion

        //DllImport
        #region
        [DllImport("StdGraphFileTransformCore1.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ProcessingDataQT(float[] Data, float[] Data_Filted, int DataNum, DetectParamter channelParam, IntPtr ptr, ref int SummitNum, int SummitMaxNum);

        [DllImport("StdGraphFileTransformCore1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ReadFYP2GraphFileQT(string FilePath, IntPtr ptrHeadInfo, IntPtr ptrChannelInfo, IntPtr[] ptrPeakInfo, IntPtr[] ptrSummit);

        //峰识别
        [DllImport("StdGraphFileTransformCore1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AnalysFYP2GraphFileQT(IntPtr ptrHeadInfo, IntPtr ptrChannelInfo, IntPtr[] ptrDataInfo, IntPtr[] ptrPeakInfo, IntPtr ptrEleBlyzInfo, int elementNum, int detParamIniNo, IntPtr ptrSpeTreatCond, int speTreatCondNum);

        //添加峰
        [DllImport("StdGraphFileTransformCore1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AddGraphApexQT(IntPtr ptrHeadInfo, IntPtr ptrChannelInfo, IntPtr[] ptrDataInfo, IntPtr[] ptrPeakInfo, int channelNo, int startIndex, int endIndex, string elementName);

        //修改峰
        [DllImport("StdGraphFileTransformCore1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ModifyGraphApexQT(IntPtr ptrHeadInfo, IntPtr ptrChannelInfo, IntPtr[] ptrDataInfo, IntPtr[] ptrPeakInfo, int channelNo, int apexIndex, int startIndex, int endIndex, string elementName);

        //删除峰
        [DllImport("StdGraphFileTransformCore1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool DelGraphApexQT(IntPtr ptrChannelInfo, IntPtr[] ptrPeakInfo, int channelNo, int apexIndex);

        //保存谱图
        [DllImport("StdGraphFileTransformCore1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SaveFYP2GraphDataFileQT(string FilePath, IntPtr ptrHeadInfo, IntPtr ptrChannelInfo, IntPtr[] ptrDataInfo, IntPtr[] ptrPeakInfo);

        //修改峰识别参数
        [DllImport("StdGraphFileTransformCore1.dll", CallingConvention = CallingConvention.Cdecl)]
        //(detParamIniNo,channelNo,paramName,paramValue);
        public static extern bool ModApexDetectParameterQT(int detParamIniNo, int channelNo, string paramName, string paramValue);

        #endregion
        static void Main(string[] args)
        {
            //diaoyongOld();

            for (int ijk=0;ijk<1;ijk++)
            {
                //Console.WriteLine("----ijk:" + ijk);
                diaoyongNew();
            }
            Console.ReadLine();
        }

        static void diaoyongOld()
        {
            //峰识别参数
            #region
            DetectParamter channelParam = new DetectParamter
            {
                HammingFilter_N = 1,
                Hamming_Band = 1.1f,
                SRCFilter_N = 1,
                Summit_Width = 1,
                Summit_Begin = 1.2f,
                Summit_End = 1.3f,
                Summit_HeightUp = 1.4f,
                Summit_RealHeightUp = 1.5f,
                Summit_HeightDown = 1.6f,
                Summit_RealHeightDown = 1.7f,
                Summit_SynLimit = 1.8f,
                databeg = 1,
                dataend = 1,
                Reserve1 = true,//甲烷峰起止点落差大于该值时才进行修正 ，单位是mV
                Reserve2 = true,   //甲烷峰起止点调整时，合适的落差范围，单位是mV
                Reserve3 = 1,  //CO组分识别时的保留时间偏移，单位为分钟
                Reserve4 = 1,         //平头峰判断电压
                Reserve5 = 1.0f,  //平头峰最小峰宽s（因无法获取绝对电压）
                Reserve6 = 1.0f           // 平头峰系数x
            };
            #endregion
            //////////////////////////////////////////////
            //峰识别
            #region
            SummitForDll[] msg1 = new SummitForDll[20];
            for (int i = 0; i < msg1.Length; i++)
            {
                msg1[i] = new SummitForDll();
            }
            IntPtr[] ptArray = new IntPtr[1];
            ptArray[0] = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SummitForDll)) * 20);
            IntPtr pt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SummitForDll)));
            Marshal.Copy(ptArray, 0, pt, 1);

            float[] Data;
            Data = new float[10] { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f, 1.1f, 2.2f, 3.3f, 4.4f, 5.5f };

            float[] Data_Filted;
            Data_Filted = new float[10] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };

            int DataNum = 10;
            int N1 = 10;
            int N2 = 300;
            ProcessingDataQT(Data, Data_Filted, DataNum, channelParam, pt, ref N1,N2);
            Marshal.FreeHGlobal(pt);
            #endregion
        }

        static void diaoyongNew()
        {
            //读谱图FYP2
            #region
            string filePath = "D:/fyp2/2019-10-23_13_43_26.fyp2";
            string filePathSave = "D:/fyp2/saveConsoleApp.fyp2";
            int maxChannelNum = 3;//最大通道数
            int maxPeakNum = 300; //最大峰个数
            int maxChannelDataNum = 144000;

            //GraphHead
            int memSizeGraphHead = Marshal.SizeOf(typeof(FYP2GraphHead));
            IntPtr ptGraphHead = Marshal.AllocHGlobal(memSizeGraphHead);

            //ChannelInfo
            int memSizeChannelInfo = Marshal.SizeOf(typeof(FYP2ChannelInfo)) * maxChannelNum;
            IntPtr ptChannelInfo = Marshal.AllocHGlobal(memSizeChannelInfo);

            //DataInfo
            IntPtr[] ptDataInfo = new IntPtr[maxChannelNum];
            for (int i = 0; i < maxChannelNum; i++)
            {
                int memSizeDataInfo = Marshal.SizeOf(typeof(float)) * maxChannelDataNum;
                ptDataInfo[i] = Marshal.AllocHGlobal(memSizeDataInfo);
            }

            //PeakInfo
            IntPtr[] ptPeakInfo = new IntPtr[maxChannelNum];
            for (int i = 0; i < maxChannelNum; i++)
            {
                int memSizePeakInfo = Marshal.SizeOf(typeof(FYP2PeakInfo)) * maxPeakNum;
                ptPeakInfo[i] = Marshal.AllocHGlobal(memSizePeakInfo);
            }

            //读谱图文件
            ReadFYP2GraphFileQT(filePath, ptGraphHead, ptChannelInfo, ptDataInfo, ptPeakInfo);

            //获取GraphHead
            FYP2GraphHead headInfo;
            headInfo = new FYP2GraphHead();
            headInfo = (FYP2GraphHead)Marshal.PtrToStructure((IntPtr)((UInt32)ptGraphHead), typeof(FYP2GraphHead));

            //获取ChannelInfo
            int channelNum = headInfo.ChannelCount;
            FYP2ChannelInfo[] channelInfo = new FYP2ChannelInfo[channelNum];
            for (int k1 = 0; k1 < channelNum; k1++)
            {
                channelInfo[k1] = new FYP2ChannelInfo();
                channelInfo[k1] = (FYP2ChannelInfo)Marshal.PtrToStructure((IntPtr)((UInt32)ptChannelInfo + k1 * Marshal.SizeOf(typeof(FYP2ChannelInfo))), typeof(FYP2ChannelInfo));
            }

            //获取DataInfo
            float[][] dataInfo = new float[channelNum][];
            for (int k1 = 0; k1 < channelNum; k1++)
            {
                int channelDataNum;
                if (channelInfo[k1].UseData == 0)
                {
                    channelDataNum = channelInfo[k1].DataNum0;
                }
                else if (channelInfo[k1].UseData == 1)
                {
                    channelDataNum = channelInfo[k1].DataNum1;
                }
                else
                {
                    channelDataNum = channelInfo[k1].DataNum2;
                }

                dataInfo[k1] = new float[channelDataNum];
                for (int k2 = 0; k2 < channelDataNum; k2++)
                {
                    dataInfo[k1][k2] = (float)Marshal.PtrToStructure((IntPtr)((UInt32)ptDataInfo[k1] + k2 * Marshal.SizeOf(typeof(float))), typeof(float));
                }
            }

            //获取PeakInfo
            FYP2PeakInfo[][] peakInfo = new FYP2PeakInfo[channelNum][];
            if (headInfo.IsAnalyse == 1)
            {
                for (int k1 = 0; k1 < channelNum; k1++)
                {
                    int channelApexNum;
                    if (channelInfo[k1].UseData == 0)
                    {
                        channelApexNum = channelInfo[k1].ApexNum;
                    }
                    else if (channelInfo[k1].UseData == 1)
                    {
                        channelApexNum = channelInfo[k1].DataNum1;
                    }
                    else
                    {
                        channelApexNum = channelInfo[k1].DataNum2;
                    }
                    peakInfo[k1] = new FYP2PeakInfo[channelApexNum];

                    for (int k2 = 0; k2 < channelApexNum; k2++)
                    {
                        peakInfo[k1][k2] = (FYP2PeakInfo)Marshal.PtrToStructure((IntPtr)((UInt32)ptPeakInfo[k1] + k2 * Marshal.SizeOf(typeof(FYP2PeakInfo))), typeof(FYP2PeakInfo));
                    }
                }
            }

            for (int i = 0; i < headInfo.ChannelCount; i++)
            {
                Console.WriteLine("AnalysFYP2GraphFileQT_ChannelInfo_{0}_ApexNum:{1}", i, channelInfo[i].ApexNum);
            }
            #endregion

            //////////////////////////////////////////////
            //峰识别FYP2
            #region
            //分析谱图文件

            //ElementBlyz
            int elementNum = 7;
            int memSizeEleBlyz = Marshal.SizeOf(typeof(SElementBlyzInfo2));
            IntPtr ptEleBlyzInfo = Marshal.AllocHGlobal(memSizeEleBlyz * elementNum);

            SElementBlyzInfo2[] elementBlyz = new SElementBlyzInfo2[elementNum];
            elementBlyz[0].elementName = "H2";
            elementBlyz[0].elementtype = 1;
            elementBlyz[0].channelno = 0;
            elementBlyz[0].blsj = 0.32f;
            elementBlyz[1].elementName = "O2";
            elementBlyz[1].elementtype = 2;
            elementBlyz[1].channelno = 1;
            elementBlyz[1].blsj = 1.32f;
            elementBlyz[2].elementName = "CO2";
            elementBlyz[2].elementtype = 3;
            elementBlyz[2].channelno = 0;
            elementBlyz[2].blsj = 2.32f;
            elementBlyz[3].elementName = "CH4";
            elementBlyz[3].elementtype = 4;
            elementBlyz[3].channelno = 1;
            elementBlyz[3].blsj = 3.32f;
            elementBlyz[4].elementName = "C2H2";
            elementBlyz[4].elementtype = 5;
            elementBlyz[4].channelno = 0;
            elementBlyz[4].blsj = 4.32f;
            elementBlyz[5].elementName = "C2H4";
            elementBlyz[5].elementtype = 6;
            elementBlyz[5].channelno = 1;
            elementBlyz[5].blsj = 5.32f;
            elementBlyz[6].elementName = "C2H6";
            elementBlyz[6].elementtype = 7;
            elementBlyz[6].channelno = 1;
            elementBlyz[6].blsj = 7.83f;
            for (int i = 0; i < elementNum; i++)
            {
                Marshal.StructureToPtr(elementBlyz[i], ptEleBlyzInfo + i * memSizeEleBlyz, true);
            }

            int detParamIniNo = 0;//使用的峰识别参数

            //ptrSpeTreatCond
            int speTreatCondNum = 0;
            int memSizeSpecTreatCond = Marshal.SizeOf(typeof(SpecialTreatCond));
            IntPtr ptSpeTreatCond = Marshal.AllocHGlobal(memSizeSpecTreatCond * speTreatCondNum);

            SpecialTreatCond[] speTreatCond = new SpecialTreatCond[speTreatCondNum];
            /*
            speTreatCond[0].channelNo = 1;
            speTreatCond[0].timeNum = 4;
            speTreatCond[0].times = new float[60];
            speTreatCond[0].times[0] = 1.0f;
            speTreatCond[0].times[1] = 2.0f;
            speTreatCond[0].times[2] = 2.0f;
            speTreatCond[0].times[3] = 4.0f;

            speTreatCond[0].elementName = "";
            speTreatCond[0].typeNoParam = "6.2,0.1";
            for (int i = 0; i < speTreatCondNum; i++)
            {
                Marshal.StructureToPtr(speTreatCond[i], ptSpeTreatCond + i * memSizeSpecTreatCond, true);
            }*/

            //新的峰识别函数，增加特殊处理功能
            AnalysFYP2GraphFileQT(ptGraphHead, ptChannelInfo, ptDataInfo, ptPeakInfo, ptEleBlyzInfo, elementNum, detParamIniNo, ptSpeTreatCond, speTreatCondNum);

            #endregion
            //////////////////////////////////////////////
            //添加峰
            /*
            int channelNo = 0;
            int startIndex = 191;
            int endIndex = 210;
            string elementName = "$$$$";
            bool rtn3 = AddGraphApexQT(ptGraphHead, ptChannelInfo, ptDataInfo, ptPeakInfo, channelNo, startIndex, endIndex, elementName);
            */

            //////////////////////////////////////////////
            //修改峰
            /*
            int channelNo = 0;
            int apexIndex = 0;
            int startIndex = 215;
            int endIndex = 580;
            string elementName = "$$$$";
            bool rtn4 = ModifyGraphApexQT(ptGraphHead,ptChannelInfo,ptDataInfo,ptPeakInfo,channelNo,apexIndex,startIndex,endIndex,elementName);
            */

            //////////////////////////////////////////////
            //删除峰
            /*
            int channelNo = 0;
            int apexIndex = 0;

            DelGraphApexQT(ptChannelInfo,ptPeakInfo,channelNo,apexIndex);
            */

            //////////////////////////////////////////////
            //保存谱图
            //SaveFYP2GraphDataFileQT(filePathSave, ptGraphHead, ptChannelInfo, ptDataInfo, ptPeakInfo);

            //////////////////////////////////////////////
            //修改峰识别参数
            int channelNo = 0;
            string paramName = "dataend";
            string paramValue = "-1";

            //ModApexDetectParameterQT(detParamIniNo,channelNo,paramName,paramValue);

            //获取GraphHead
            headInfo = (FYP2GraphHead)Marshal.PtrToStructure((IntPtr)((UInt32)ptGraphHead), typeof(FYP2GraphHead));

            //获取ChannelInfo
            int channelNum2 = headInfo.ChannelCount;
            for (int k1 = 0; k1 < channelNum; k1++)
            {
                channelInfo[k1] = new FYP2ChannelInfo();
                channelInfo[k1] = (FYP2ChannelInfo)Marshal.PtrToStructure((IntPtr)((UInt32)ptChannelInfo + k1 * Marshal.SizeOf(typeof(FYP2ChannelInfo))), typeof(FYP2ChannelInfo));
            }

            //获取PeakInfo
            if (headInfo.IsAnalyse == 1)
            {
                for (int k1 = 0; k1 < channelNum2; k1++)
                {
                    int channelApexNum;
                    channelApexNum = channelInfo[k1].ApexNum;
                    peakInfo[k1] = new FYP2PeakInfo[channelApexNum];

                    for (int k2 = 0; k2 < channelApexNum; k2++)
                    {
                        peakInfo[k1][k2] = (FYP2PeakInfo)Marshal.PtrToStructure((IntPtr)((UInt32)ptPeakInfo[k1] + k2 * Marshal.SizeOf(typeof(FYP2PeakInfo))), typeof(FYP2PeakInfo));
                    }
                }
            }

            //打印信息
            for (int i = 0; i < headInfo.ChannelCount; i++)
            {
                Console.WriteLine("AnalysFYP2GraphFileQT_ChannelInfo_{0}_ApexNum:{1}", i, channelInfo[i].ApexNum);
                for (int j = 0; j < channelInfo[i].ApexNum; j++)
                {
                    Console.WriteLine("AnalysFYP2GraphFileQT_StartInex:{0}__EndIndex:{1}", peakInfo[i][j].StartIndex, peakInfo[i][j].EndIndex);
                }
            }
            //释放内存
            Marshal.FreeHGlobal(ptGraphHead);
            Marshal.FreeHGlobal(ptChannelInfo);
            for (int k3 = 0; k3 < maxChannelNum; k3++)
            {
                Marshal.FreeHGlobal(ptDataInfo[k3]);
            }
            for (int k3 = 0; k3 < maxChannelNum; k3++)
            {
                Marshal.FreeHGlobal(ptPeakInfo[k3]);
            }
            Marshal.FreeHGlobal(ptEleBlyzInfo);
            Marshal.FreeHGlobal(ptSpeTreatCond);


        }
    }
}
