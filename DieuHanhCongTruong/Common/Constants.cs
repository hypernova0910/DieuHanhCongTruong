using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Common
{
    class Constants
    {
        public static Color SELECTED_COLOR = Color.FromArgb(4, 139, 205);
        public static Color MENU_BACKGROUND_COLOR = Color.FromArgb(250, 247, 240);

        public static string TINHTRANG_DAXULY = "Đã xử lý";
        public static string TINHTRANG_CHUAXULY = "Chưa xử lý";

        public static string PPXULY_HUYTAICHO = "Hủy tại chỗ";
        public static string PPXULY_THUGOM = "Thu gom";

        public static string[] MACHINE_COLORS = {
            "#A2231D",
            "#2271B3",
            "#D0D0D0",
            "#2C5545",
            "#A03472",
            "#A18594",
            "#898176",
            "#F3DA0B",
            "#955F20",
            "#A98307",
            "#49678D",
            "#FFA420",
            "#F5D033",
            "#89AC76",
            "#212121",
            "#4C9141",
            "#8A6642",
            "#6C7059",
        };

        public static Color[] magnetic_colors =
        {
            Color.FromArgb(0,0,192),
            Color.FromArgb(0,0,255),
            Color.FromArgb(0,48,255),
            Color.FromArgb(0,96,255),
            Color.FromArgb(0,144,255),
            Color.FromArgb(0,192,255),
            Color.FromArgb(0,255,255),
            Color.FromArgb(48,255,192),
            Color.FromArgb(96,255,144),
            Color.FromArgb(144,255,96),
            Color.FromArgb(192,255,48),
            Color.FromArgb(255,255,0),
            Color.FromArgb(255,204,0),
            Color.FromArgb(255,136,0),
            Color.FromArgb(255,85,0),
            Color.FromArgb(255,0,0),
        };

        public static string ICON_FOLDER = "machine icons extended";

        public static double MAX_Z_BOMB = 14083.0212603552;
        public static double MIN_Z_BOMB = -18359.2485593716;
    }
}
