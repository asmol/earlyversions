using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gameserver
{
    static class Utilities
    {
        public static void CutFromStart<T>(ref T[] array, int length)
        {
            if (length > array.Length)
                return;
            int resultLength = array.Length-length;
            T[] result = new T[resultLength];
            Array.Copy(array,length,result,0,resultLength);
            array = result;
        }

        public static void Append<T>(ref T[] target, int targetIndex, T[] source)
        {
            Array.Resize(ref target,targetIndex+source.Length);
            Array.Copy(source,target,source.Length);
        }

        public static void Invoke(Control target, Action action)
        {
            if (target.InvokeRequired)
                target.Invoke((MethodInvoker)delegate() {Invoke(target,action);});
            else
                action();
        }
    }
}