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
    }
}
