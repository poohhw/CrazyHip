
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CrazyHip.Tool
{
    public class Mapping
    {
        /// <summary>
        /// 서로 다른 클래스의 프로퍼티 이름이 같은 값을 매핑처리합니다.
        /// </summary>
        /// <typeparam name="Tin">참조 클래스 형식을 지정</typeparam>
        /// <typeparam name="TOut">리턴 클래스 형식을 지정</typeparam>
        /// <param name="a">참조 클래스</param>
        /// <param name="b">리턴 클래스</param>
        /// <returns></returns>
        public static TOut CopyProp<Tin, TOut>(Tin a, TOut b)
        {
            Type typeB = b.GetType();

            foreach (PropertyInfo property in a.GetType().GetProperties())
            {
                if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                    continue;

                PropertyInfo other = typeB.GetProperty(property.Name);
                if ((other != null) && (other.CanWrite))
                    other.SetValue(b, property.GetValue(a, null), null);
            }

            return (TOut)b;
        }

    }
}