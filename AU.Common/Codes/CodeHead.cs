/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����CodeHead.cs
        // �ļ�����������ָ��ͷ����������ͨѶ���Է��յ�ָ���ʱ���ȶ�ȡ����ָ��ͷ��ͨ��ָ��ͷ��ʶ���������á�
//----------------------------------------------------------------*/

namespace AU.Common.Codes
{
    /// <summary>
    /// ָ���ͷ��
    /// </summary>
    public enum CodeHead
    {

        #region ��ͨѶ��

        /// <summary>
        /// ��ȡ����������Ϣ(��������IP)
        /// </summary>
        HOST_MESSAGE,
        /// <summary>
        /// �ػ�
        /// </summary>
        SHUTDOWN,
        /// <summary>
        /// ����
        /// </summary>
        REBOOT,

        /// <summary>
        /// �뿪
        /// </summary>
        EXIT,
        /// <summary>
        /// ���ӳɹ�
        /// </summary>
        CONNECT_OK,
        /// <summary>
        /// �ر�����
        /// </summary>
        CONNECT_CLOSE,
        /// <summary>
        /// ���½�������
        /// </summary>
        CONNECT_RESTART,

        /// <summary>
        /// �رճ���
        /// </summary>
        CLOSE_APPLICATION,

        /// <summary>
        /// ����ʧ��
        /// </summary>
        FAIL,
        /// <summary>
        /// ��������
        /// </summary>
        PASSWORD,
        /// <summary>
        /// �����޸ĳɹ�
        /// </summary>
        CHANGE_PASSWORD_OK,
        /// <summary>
        /// ���жԻ�
        /// </summary>
        SPEAK,

        /// <summary>
        /// ��Ļ���Ƶ�׼�������Ѿ����
        /// </summary>
        SCREEN_READY,
        /// <summary>
        /// ����Ļ��ȡ
        /// </summary>
        SCREEN_OPEN,
        /// <summary>
        /// �ر���Ļ��ȡ
        /// </summary>
        SCREEN_CLOSE,
        /// <summary>
        /// ��Ļ����ʧ��
        /// </summary>
        SCREEN_FAIL,
        /// <summary>
        /// ��Ļ���ͳɹ�
        /// </summary>
        SCREEN_SUCCESS,
        /// <summary>
        /// �����ȡ��Ļ
        /// </summary>
        SCREEN_GET,

        /// <summary>
        /// ���·����
        /// </summary>
        UPDATE,
        /// <summary>
        /// �����Ѿ�׼����
        /// </summary>
        UPDATE_READY,
        /// <summary>
        /// ����ʧ��
        /// </summary>
        UPDATE_FAIL,
        /// <summary>
        /// ����˵İ汾
        /// </summary>
        VERSION,

        #endregion

        #region �����������
        /// <summary>
        /// �������
        /// </summary>
        CONTROL_MOUSE,
        /// <summary>
        /// ���Ƽ���
        /// </summary>
        CONTROL_KEYBOARD,

        #endregion

        #region �ļ�����

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        GET_DISKS,
        /// <summary>
        /// ���ʹ�����Ϣ
        /// </summary>
        SEND_DISKS,
        /// <summary>
        /// �������(�ļ���·��)
        /// </summary>
        GET_DIRECTORY_DETIAL,
        /// <summary>
        /// �����ļ����ڵ���Ϣ
        /// </summary>
        SEND_DIRECTORY_DETIAL,
        /// <summary>
        /// ��ȡ�ļ���ϸ��Ϣ
        /// </summary>
        GET_FILE_DETIAL,
        /// <summary>
        /// �����ļ���ϸ��Ϣ
        /// </summary>        
        SEND_FILE_DETIAL,

        /// <summary>
        /// ������ȡ�ļ�������
        /// </summary>
        GET_FILE,
        /// <summary>
        /// ���������ļ�������
        /// </summary>
        SEND_FILE,
        /// <summary>
        /// ��ȡ�ļ��ķ�����Ѿ�׼����
        /// </summary>
        GET_FILE_READY,
        /// <summary>
        /// �����ļ��ķ�����Ѿ�׼����
        /// </summary>
        SEND_FILE_READY,
        /// <summary>
        /// �ļ��������
        /// </summary>
        FILE_TRAN_END,

        #endregion

    }
}
