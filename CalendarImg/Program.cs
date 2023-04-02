using System.Drawing;

namespace CalendarImg
{
    static class CalendarImg
    {
        static void Main()
        {
            int year = 2023;
            int month = 4;

            // 画像のサイズを指定
            int width = 800;
            int height = 800;

            // 画像を生成
            using Bitmap bitmap = new Bitmap(width, height);
            using Graphics graphics = Graphics.FromImage(bitmap);
            // カレンダーの描画
            DrawCalendar(graphics, year, month, width, height);

            // 画像の保存
            bitmap.Save("calendar.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        static void DrawCalendar(Graphics graphics, int year, int month, int width, int height)
        {
            // カレンダーの背景色を設定
            graphics.Clear(Color.FromArgb(200, 255, 200));

            // カレンダーの表示範囲を計算
            var startOfMonth = new DateTime(year, month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            var startOfCalendar = startOfMonth.AddDays(-(int)startOfMonth.DayOfWeek);
            var endOfCalendar = endOfMonth.AddDays(6 - (int)endOfMonth.DayOfWeek);

            // カレンダーの日付を描画
            int cellWidth = width / 7;
            int cellHeight = height / (((int)endOfCalendar.Subtract(startOfCalendar).TotalDays / 7) + 1);
            for (var date = startOfCalendar; date <= endOfCalendar; date = date.AddDays(1))
            {
                Brush brush = Brushes.Black;
                if (date.Month != month)
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        brush = new SolidBrush(Color.FromArgb(128, 128, 255));
                    }
                    else if (date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        brush = new SolidBrush(Color.FromArgb(255, 128, 128));
                    }
                    else
                    {
                        brush = new SolidBrush(Color.FromArgb(128, 128, 128));
                    }
                }
                else
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        brush = Brushes.Blue;
                    }
                    else if (date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        brush = Brushes.Red;
                    }
                }

                string dayString = date.Day.ToString();
                SizeF size = graphics.MeasureString(dayString, new Font("Arial", cellHeight / 2));
                float x = (float)(((date.DayOfWeek - DayOfWeek.Sunday) * cellWidth) + ((cellWidth - size.Width) / 4));
                float y = (float)(((int)date.Subtract(startOfCalendar).TotalDays / 7 * cellHeight) + ((cellHeight - size.Height) / 2));
                graphics.DrawString(dayString, new Font("Arial", cellHeight / 3), brush, x, y);
            }
        }
    }
}