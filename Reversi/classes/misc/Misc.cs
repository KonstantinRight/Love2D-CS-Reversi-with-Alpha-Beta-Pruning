using System;
using System.Collections.Generic;
using System.Linq;

namespace Reversi.classes.misc
{
    public static class Misc
    {
        public static bool IsElementBetweenTwo(int[] ElPos, int[] FirstPos, int[] SecPos)
        {
            var res = false;
            //if (IsElementBetweenTwoVertically(ElPos, FirstPos, SecPos))
                //Console.WriteLine("yep");
            res = res || IsElementBetweenTwoHorizontally(ElPos, FirstPos, SecPos);
            res = res || IsElementBetweenTwoVertically(ElPos, FirstPos, SecPos);
            res = res || IsElementBetweenTwoMainDiagonal(ElPos, FirstPos, SecPos);
            res = res || IsElementBetweenTwoAntiDiagonal(ElPos, FirstPos, SecPos);
            return res;
        }
        private static bool IsElementBetweenTwoHorizontally(int[] ElPos, int[] FirstPos, int[] SecPos)
        {
            return (ElPos[0] > FirstPos[0] && ElPos[0] < SecPos[0] &&  ElPos[1] == FirstPos[1] && ElPos[1] == SecPos[1] ||
                ElPos[0] > SecPos[0] && ElPos[0] < FirstPos[0] && ElPos[1] == FirstPos[1] && ElPos[1] == SecPos[1]);
        }
        private static bool IsElementBetweenTwoVertically(int[] ElPos, int[] FirstPos, int[] SecPos)
        {
            return (ElPos[1] > FirstPos[1] && ElPos[1] < SecPos[1] && ElPos[0] == FirstPos[0] && ElPos[0] == SecPos[0] ||
                ElPos[1] > SecPos[1] && ElPos[1] < FirstPos[1] && ElPos[0] == FirstPos[0] && ElPos[0] == SecPos[0]);
        }
        private static bool IsElementBetweenTwoMainDiagonal(int[] ElPos, int[] FirstPos, int[] SecPos)
        {
            if (IsMainDiagonal(FirstPos, SecPos) && IsMainDiagonal(ElPos, FirstPos))
            {
                return (ElPos[0] > FirstPos[0] && ElPos[1] > FirstPos[1] && ElPos[0] < SecPos[0] && ElPos[1] < SecPos[1] ||
                ElPos[0] > SecPos[0] && ElPos[1] > SecPos[1] && ElPos[0] < FirstPos[0] && ElPos[1] < FirstPos[1]);
            }
            return false;
        }
        private static bool IsElementBetweenTwoAntiDiagonal(int[] ElPos, int[] FirstPos, int[] SecPos)
        {
            if (IsAntiDiagonal(FirstPos, SecPos) && IsAntiDiagonal(ElPos, FirstPos))
            {
                return (ElPos[0] > FirstPos[0] && ElPos[1] < FirstPos[1] && ElPos[0] < SecPos[0] && ElPos[1] > SecPos[1] ||
                ElPos[0] > SecPos[0] && ElPos[1] < SecPos[1] && ElPos[0] < FirstPos[0] && ElPos[1] > FirstPos[1]);
            }
            return false;
        }
        //Main and Anti here and there are about direction, not related with matrix definition fully
        private static bool IsMainDiagonal(int[] FirstPos, int[] SecPos)
        {
            return SecPos[0] - FirstPos[0] == SecPos[1] - FirstPos[1] || FirstPos[0] - SecPos[0] == FirstPos[1] - SecPos[1];
        }

        private static bool IsAntiDiagonal(int[] FirstPos, int[] SecPos)
        {
            return SecPos[0] - FirstPos[0] != SecPos[1] - FirstPos[1] && Math.Abs(SecPos[0] - FirstPos[0]) == Math.Abs(SecPos[1] - FirstPos[1]) ||
                FirstPos[0] - SecPos[0] != FirstPos[1] - SecPos[1] && Math.Abs(FirstPos[0] - SecPos[0]) == Math.Abs(FirstPos[1] - SecPos[1]);
        }
    }
}
