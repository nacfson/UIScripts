using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButton
{
    /// <summary>
    /// ���õ��� ��
    /// </summary>
    void Selected();

    /// <summary>
    /// ���õ��� �ʾ��� ��
    /// </summary>
    void NoneSelected();
}
