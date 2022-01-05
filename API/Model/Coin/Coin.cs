using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public abstract class Coin
    {
        public int type { get; } //APENAS PARA VER O TIPO DE MOEDA
        public float value { get; set; }
        private abstract float getXEur();
        private abstract float getXUsd();
        private abstract float getXGbp();
        private abstract float getXCnh();
        private abstract float getXJpy();
        private abstract float getXAda();
        private abstract float getXBtc();

        protected Coin(){
            this.type = 1;
            this.value = 0.0;
        }
        protected Coin(int t){
            this.type = t;
            this.value = 0.0;
        }
        protected Coin(int t,float value){
            this.type = t;
            this.value = value;
        }
        

        public void exchangeEUR(){
            float s = this.value * getXEur();
            this = new EUR(s);
        }
        public void exchangeUSD(){
            float s = this.value * getXUsd();
            this = new USD(s);
        }
        public void exchangeGBP(){
            float s = this.value * getXGbp();
            this = new GBP(s);
        }
        public void exchangeCNH(){
            float s = this.value * getXCnh();
            this = new CNH(s);
        }
        public void exchangeJPY(){
            float s = this.value * getXJpy();
            this = new JPY(s);
        }
        public void exchangeADA(){
            float s = this.value * getXAda();
            this = new ADA(s);
        }
        public void exchangeBTC(){
            float s = this.value * getXBtc();
            this = new BTC(s);
        }

        public void addVal(float v){
            this.value += v;
        }
        public void takeVal(float v){
            this.value -= v;
        }
    }

    //1
    public class EUR : Coin
    {
        private static float eur;
        private static float usd;
        private static float gbp;
        private static float cnh;
        private static float jpy;
        private static float ada;
        private static float btc;

        public EUR() : base(1){}
        public EUR(float value) : base(1,value){}
        static EUR(){
            eur = 1.0;
            usd = 1.1290739;
            gbp = 0.8522250;
            cnh = 7.1928394;
            jpy = 128.4069300;
            ada = 0.89595;
            btc = 0.000023016139836;
        }

        public float getXEur(){return this.eur;}
        public float getXUsd(){return this.usd;}
        public float getXGbp(){return this.gbp;}
        public float getXCnh(){return this.cnh;}
        public float getXJpy(){return this.jpy;}
        public float getXAda(){return this.ada;}
        public float getXBtc(){return this.btc;}
    }

    //2
    public class USD : Coin
    {
        private static float eur;
        private static float usd;
        private static float gbp;
        private static float cnh;
        private static float jpy;
        private static float ada;
        private static float btc;

        public USD() : base(2){}
        public USD(float value) : base(2,value){}
        static USD(){
            usd = 1.0;
            eur = 1/1.1290739;
            gbp = 0.7546659;
            cnh = 6.3717505;
            jpy = 113.73591;
            ada = 0.7880;
            btc = 0.0000203286;
        }

        public float getXEur(){return this.eur;}
        public float getXUsd(){return this.usd;}
        public float getXGbp(){return this.gbp;}
        public float getXCnh(){return this.cnh;}
        public float getXJpy(){return this.jpy;}
        public float getXAda(){return this.ada;}
        public float getXBtc(){return this.btc;}
    }

    //3
    public class GBP : Coin
    {
        private static float eur;
        private static float usd;
        private static float gbp;
        private static float cnh;
        private static float jpy;
        private static float ada;
        private static float btc;

        public GBP() : base(3){}
        public GBP(float value) : base(3,value){}
        static GBP(){
            gbp = 1.0;
            eur = 1/0.8522250;
            usd = 1/0.7546659;
            cnh = 8.4430207;
            jpy = 150.71424;
            ada = 1.0440;
            btc = 0.000026931368639;
        }

        public float getXEur(){return this.eur;}
        public float getXUsd(){return this.usd;}
        public float getXGbp(){return this.gbp;}
        public float getXCnh(){return this.cnh;}
        public float getXJpy(){return this.jpy;}
        public float getXAda(){return this.ada;}
        public float getXBtc(){return this.btc;}
    }

    //4
    public class CNH : Coin
    {
        private static float eur;
        private static float usd;
        private static float gbp;
        private static float cnh;
        private static float jpy;
        private static float ada;
        private static float btc;

        public CNH() : base(4){}
        public CNH(float value) : base(4,value){}
        static CNH(){
            cnh = 1.0;
            eur = 1/7.1928394;
            usd = 1/6.3717505;
            gbp = 1/8.4430207;
            jpy = 17.849683;
            ada = 0.1239;
            btc = 0.000003186748378;
        }

        public float getXEur(){return this.eur;}
        public float getXUsd(){return this.usd;}
        public float getXGbp(){return this.gbp;}
        public float getXCnh(){return this.cnh;}
        public float getXJpy(){return this.jpy;}
        public float getXAda(){return this.ada;}
        public float getXBtc(){return this.btc;}
    }

    //5
    public class JPY : Coin
    {
        private static float eur;
        private static float usd;
        private static float gbp;
        private static float cnh;
        private static float jpy;
        private static float ada;
        private static float btc;

        public JPY() : base(5){}
        public JPY(float value) : base(5,value){}
        static JPY(){
            jpy = 1.0;
            eur = 1/128.4069300;
            usd = 1/113.73591;
            gbp = 1/150.71424;
            cnh = 1/17.849683;
            ada = 0.0069;
            btc = 0.000000178884724;
        }

        public float getXEur(){return this.eur;}
        public float getXUsd(){return this.usd;}
        public float getXGbp(){return this.gbp;}
        public float getXCnh(){return this.cnh;}
        public float getXJpy(){return this.jpy;}
        public float getXAda(){return this.ada;}
        public float getXBtc(){return this.btc;}
    }

    //6
    public class ADA : Coin
    {
        private static float eur;
        private static float usd;
        private static float gbp;
        private static float cnh;
        private static float jpy;
        private static float ada;
        private static float btc;

        public ADA() : base(6){}
        public ADA(float value) : base(6,value){}
        static ADA(){
            ada = 1.0;
            eur = 1/0.89595;
            usd = 1/0.7880;
            gbp = 1/1.0440;
            cnh = 1/0.1239;
            jpy = 1/0.0069;
            btc = 0.000026;
        }

        public float getXEur(){return this.eur;}
        public float getXUsd(){return this.usd;}
        public float getXGbp(){return this.gbp;}
        public float getXCnh(){return this.cnh;}
        public float getXJpy(){return this.jpy;}
        public float getXAda(){return this.ada;}
        public float getXBtc(){return this.btc;}
    }

    //7
    public class BTC : Coin
    {
        private static float eur;
        private static float usd;
        private static float gbp;
        private static float cnh;
        private static float jpy;
        private static float ada;
        private static float btc;

        public BTC() : base(7){}
        public BTC(float value) : base(7,value){}
        static BTC(){
            btc = 1.0;
            eur = 1/0.000023016139836;
            usd = 1/0.0000203286;
            gbp = 1/0.000026931368639;
            cnh = 1/0.000003186748378;
            jpy = 1/0.000000178884724;
            ada = 1/0.000026;
        }

        public float getXEur(){return this.eur;}
        public float getXUsd(){return this.usd;}
        public float getXGbp(){return this.gbp;}
        public float getXCnh(){return this.cnh;}
        public float getXJpy(){return this.jpy;}
        public float getXAda(){return this.ada;}
        public float getXBtc(){return this.btc;}
    }
}