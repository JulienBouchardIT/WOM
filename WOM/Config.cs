using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOM
{
    class Config
    {
        private static Config config = new Config();

        public void saveConfig()
        {
            //todo
        }

        public static Config getConfig()
        {
            return config;
        }

        private Config()
        {

        }


    }
}
