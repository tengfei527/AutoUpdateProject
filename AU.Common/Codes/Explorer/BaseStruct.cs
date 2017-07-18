using System;
/// <summary>
/// �ļ������������漰���ļ������ָ������ļ��ṹ��
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// ��־(�ļ����ļ���)
    /// </summary>
    public enum FileFlag
    {
        /// <summary>
        /// �ļ�
        /// </summary>
        File,
        /// <summary>
        /// �ļ���
        /// </summary>
        Directory,
        /// <summary>
        /// ����
        /// </summary>
        Disk,
    }
    /// <summary>
    /// �ļ�����ṹ
    /// </summary>
    [Serializable]
    public abstract class BaseFile
    {
        /// <summary>
        /// ��־(�ļ�,�ļ���,����)
        /// </summary>
        public abstract FileFlag Flag
        {
            get;
        }
        /// <summary>
        /// ȫ��
        /// </summary>
        public abstract string Name
        {
            get;
        }
    }
}
