using System;

namespace RDPalgorithm
{
    public struct MyPoint
    {
        public int X { get; set; }
        public short Y { get; set; }
    }

    public static class RDPAlgorithm
    {
        /// <summary>
        /// Функция рассчета расстояния от точки до прямой, задаваемой двумя точками
        /// </summary>
        private static double PerpendicularDistance(MyPoint point, MyPoint start, MyPoint end)
        {
            if (start.X == end.X && start.Y == end.Y)
                return Math.Sqrt((point.X - start.X)*(point.X - start.X) + (point.Y - start.Y)*(point.Y - start.Y));

            float n = Math.Abs((end.X - start.X)*(start.Y - point.Y) - (start.X - point.X)*(end.Y - start.Y));
            float d = (float) Math.Sqrt((end.X - start.X)*(end.X - start.X) + (end.Y - start.Y)*(end.Y - start.Y));
            return n/d;
        }


        /// <summary>
        /// Алгоритм Рамера — Дугласа — Пекера, для уменьшения точек кривой
        /// </summary>
        /// <param name="pointList">Массив точек, задающий кривую</param>
        /// <param name="startInd">Индекс начала кривой</param>
        /// <param name="endInd">Индекс конца кривой</param>
        /// <param name="epsilon">Точность</param>
        /// <returns></returns>
        public static MyPoint[] DouglasPeucker(MyPoint[] pointList, int startInd, int endInd, float epsilon)
        {
            //Находим точку с максимальным расстоянием от прямой между первой и последней точками набора
            double dmax = 0;
            int index = 0;
            for (int i = 1; i < endInd - startInd; i++)
            {
                double d = PerpendicularDistance(pointList[i + startInd], pointList[startInd], pointList[endInd]);
                if (d > dmax)
                {
                    index = i + startInd;
                    dmax = d;
                }
            }

            //Если максимальная дистанция больше, чем epsilon, то рекурсивно вызываем её на участках
            if (dmax >= epsilon)
            {
                MyPoint[] recResults1 = DouglasPeucker(pointList, startInd, index, epsilon);
                MyPoint[] recResults2 = DouglasPeucker(pointList, index, endInd, epsilon);

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
                resultR[0] = pointList[startInd];
                resultR[1] = pointList[endInd];
                return resultR;
            }
        }
    }
}
