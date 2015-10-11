using System;

namespace RDPalgorithm
{
    public struct MyPoint
    {
        public int X;
        public int Y;
    }
    public static class RDPAlgorithm
    {
        private static double PerpendicularDistance(MyPoint point, MyPoint start, MyPoint end)
        {
            if (start.X == end.X && start.Y == end.Y)
            {
                return Math.Sqrt(Math.Pow((point.X - start.X), 2) + Math.Pow((point.Y - start.Y), 2));
            }

            float n = Math.Abs((end.X - start.X) * (start.Y - point.Y) - (start.X - point.X) * (end.Y - start.Y));
            float d = (float)Math.Sqrt((end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y));
            return n / d;
        }

        public static MyPoint[] DouglasPeucker(MyPoint[] pointList, float epsilon)
        {

            //Находим точку с максимальным расстоянием от прямой между первой и последней точками набора
            double dmax = 0;
            int index = 0;
            for (int i = 1; i < pointList.Length - 1; i++)
            {
                double d = PerpendicularDistance(pointList[i], pointList[0], pointList[pointList.Length - 1]);
                if (d > dmax)
                {
                    index = i;
                    dmax = d;
                }
            }

            //Если максимальная дистанция больше, чем epsilon, то рекурсивно вызываем её на участках
            if (dmax >= epsilon)
            {
                //делим последовательность на две
                MyPoint[] MyPointList1 = new MyPoint[index + 1];
                for (int i = 0; i < index + 1; i++)
                    MyPointList1[i] = pointList[i];

                MyPoint[] MyPointList2 = new MyPoint[pointList.Length - index];
                for (int i = 0; i < pointList.Length - index; i++)
                    MyPointList2[i] = pointList[index + i];

                MyPoint[] recResults1 = DouglasPeucker(MyPointList1, epsilon);
                MyPoint[] recResults2 = DouglasPeucker(MyPointList2, epsilon);

                MyPoint[] result = new MyPoint[recResults1.Length + recResults2.Length - 1];

                for (int i = 0; i < recResults1.Length; i++)
                    result[i] = recResults1[i];

                for (int i = 1; i < recResults2.Length; i++)
                    result[i + recResults1.Length - 1] = recResults2[i];

                return result;
            }
            else
            {
                MyPoint[] resultR = new MyPoint[2];
                resultR[0] = pointList[0];
                resultR[1] = pointList[pointList.Length - 1];
                return resultR;
            }
        }
    }
}
