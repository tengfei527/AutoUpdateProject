using System;
using System.Collections.Generic;
using System.Text;

namespace AU.Common
{
    /// <summary>
    /// 子系统类别
    /// </summary>
    public class SubSystem
    {
        /// <summary>
        /// 系统字典
        /// </summary>
        public static readonly Dictionary<string, string> Dic = new Dictionary<string, string>() {
            //API服务器
            { "coreserver", "E7服务器"},
            //管理中心
            {"managerserver", "E7管理中心"},
            //图象服务器
            {"imageserver", "E7图像服务器"},
            //手持机
            { "handsetserver", "E7手持机"},
            //车管系统
            { "vmsclient","E7车场"},
            //门禁系统
            { "dmsclient" , "E7门禁"},
            //消费系统
            { "cmsclient" , "E7消费"},
            //考勤系统
            {"wmsclient", "E7考勤" },
            //巡更
            { "kwsclient", "E7巡更"},
            //访客
            { "vtsclient", "E7访客"},
            //通道
            { "pwsclient", "E7通道"},
            //车位引导
            {"pgsclient", "E7车位引导"},
            //反向巡车
            {"rpcsclient", "E7反向巡车" },
            //灯光引导
            { "lgsclient", "E7灯光引导"},
        };
        /*
          /// <summary>
        /// 管理中心
        /// </summary>
        Mc = 0,

        /// <summary>
        /// 车管系统
        /// </summary>
        Vms = 1,

        /// <summary>
        /// 门禁系统
        /// </summary>
        Dms = 2,
        /// <summary>
        /// 消费系统
        /// </summary>
        Cms=3,
        /// <summary>
        /// 考勤系统
        /// </summary>
        Wms=4,
        /// <summary>
        /// 分体式门禁系统
        /// </summary>
        Sms=5,
        /// <summary>
        /// 一体式门禁
        /// </summary>
        Ims=6,
        /// <summary>
        /// 电梯
        /// </summary>
        Ems=7,
        /// <summary>
        /// 指纹门禁
        /// </summary>
        Bms=8,
        /// <summary>
        /// 巡更
        /// </summary>
        Kws=9,
        /// <summary>
        /// 访客
        /// </summary>
        Vts =10,
        /// <summary>
        /// 通道
        /// </summary>
        Pws=11,
        /// <summary>
        /// 车位引导
        /// </summary>
        Pgs = 12,
        /// <summary>
        /// 反向巡车
        /// </summary>
        Rpcs = 13,
        /// <summary>
        /// 灯光引导
        /// </summary>
        Lgs = 14
             */
        /// <summary>
        /// 系统发布字典
        /// </summary>
        public static readonly Dictionary<string, int> DicPublishType = new Dictionary<string, int>() {
            //管理中心
            {"managerserver", 0x01},
            //车管系统
            { "vmsclient",0x11},
            //门禁系统
            { "dmsclient" , 0x21},
            //消费系统
            { "cmsclient" ,0x31},
            //考勤系统
            {"wmsclient",0x41 },
            //手持机
            { "handsetserver", 0x5},
            //API服务器
            { "coreserver", 0x61},
            //图象服务器
            {"imageserver", 0x71},
            //消息服务器
            { "mqserver",0x81},
            //巡更
            { "kwsclient", 0x91},
            //访客
            { "vtsclient", 0xA1},
            //通道
            { "pwsclient", 0xB1},
            //车位引导
            {"pgsclient", 0xC1},
            //反向巡车
            {"rpcsclient", 0xD1},
            //灯光引导
            { "lgsclient", 0xE1},
        };
    }
}
