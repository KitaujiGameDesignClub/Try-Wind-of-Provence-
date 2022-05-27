using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PureAmaya.General
{
    public class ExMath
    {
        /// <summary>
        /// �����������͵ľ���ֵ
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int Abs(int index)
        {
            if (index < 0)
            {
                return -index;
            }
            else
            {
                return index;
            }
        }

        /// <summary>
        /// ���ص����ȸ���ľ���ֵ
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static float Abs(float index)
        {
            if (index < 0)
            {
                return -index;
            }
            else
            {
                return index;
            }
        }

        /// <summary>
        /// �ڸ������ȷ�Χ�ڣ��ж������������Ƿ���ͬ
        /// </summary>
        /// <param name="precision">�жϾ���</param>
        /// <param name="allowEqual">�Ƿ������ֵ���</param>
        /// <returns></returns>
        public static bool Approximation(float precision, float value1, float value2, bool allowEqual = true)
        {
            switch (allowEqual)
            {
                case true:
                    return Abs(value1 - value2) <= precision;

                case false:
                    return Abs(value1 - value2) < precision;

            }

        }

        /// <summary>
        /// ָ���������㣨�������ܸߵ㣩
        /// </summary>
        /// <param name="exponent">ָ��</param>
        /// <param name="Base">����</param>
        /// <returns></returns>
        public static int ExponentialFunction(int exponent, int Base = 10)
        {
            for (int i = 0; i < exponent; i++)
            {
                Base *= Base;
            }

            return Base;

        }


        /// <summary>
        /// �Ƿ��ڸ����ķ�Χ�ڣ������䣩
        /// </summary>
        /// <param name="value">�Ƚϵ�ֵ</param>
        /// <param name="min">��Сֵ������</param>
        /// <param name="max">���ֵ������</param>
        /// <param name="fixMinMax">�޸������С������</param>
        /// <returns></returns>
        public static bool InRange(float value, float min, float max, bool fixMinMax = true)
        {

            switch (fixMinMax)
            {
                case true:
                    if (min > max)
                    {
                        float S = min;
                        min = max;
                        max = S;
                    }
                    break;

                case false:
                    if (min > max)
                    {
                        Debug.LogError("��Сֵ�������ֵ");
                    }
                    break;
            }


            if (value >= min && value <= max)
            {
                return true;
            }
            else
            {
                return
                    false;
            }
        }

        /// <summary>
        /// �Ƕ�ת����
        /// </summary>
        /// <param name="angleDeg">�ǶȽ�</param>
        /// <param name="useAccuratePi">ʹ�þ�ȷ��Բ����</param>
        /// <returns></returns>
        public static float Deg2Rad(float angleDeg, bool useAccuratePi = false)
        {
            if (useAccuratePi)
            {
                return 3.14f / 180f * angleDeg;
            }
            else
            {
                return Mathf.Deg2Rad * angleDeg;
            }
        }

    }
}
