using System;
/// <summary>
/// �ļ�����������ָ���ࣨ������˺Ϳͻ���֮������Լ�õ�һ���涨����Ʃ��ͻ��˷��͵�ͨѶ���ݰ���ָ��SHUTDOWN��
/// ��ô������յ����������ݺ�������ػ�����Ӧ��
/// </summary>
namespace AU.Common.Codes
{  
    /// <summary>
    /// ָ��
    /// </summary>
    [Serializable]
    public abstract class Code
    {
        /// <summary>
        /// ָ��ͷ��
        /// </summary>
        public abstract CodeHead Head
        {
            get;
            set;
        }
    }
    
    /// <summary>
    /// ��ָ��
    /// </summary>
    [Serializable]
    public class BaseCode:Code
    {
        private CodeHead head;
        /// <summary>
        /// ָ��ͷ��
        /// </summary>
        public override CodeHead Head
        {
            get { return head; }
            set { head = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Head=" + head;
        }
    }

}
