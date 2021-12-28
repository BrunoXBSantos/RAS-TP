using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class Wallet{

        public float eur { get; set; }
        public float usd { get; set; }
        public float gbp { get; set; }
        public float cnh { get; set; }
        public float jpy { get; set; }
        public float ada { get; set; }
        public float btc { get; set; }

        public Wallet()
        {
            this.eur = 0.0;
            this.usd = 0.0;
            this.gbp = 0.0;
            this.cnh = 0.0;
            this.jpy = 0.0;
            this.ada = 0.0;
            this.btc = 0.0;
        }
        public Wallet(float eur, float usd, float gbp, float cnh, float jpy, float ada, float btc)
        {
            this.eur = eur;
            this.usd = usd;
            this.gbp = gbp;
            this.cnh = cnh;
            this.jpy = jpy;
            this.ada = ada;
            this.btc = btc;
        }
        
        public void addCoin(Coin c){
            switch(c.type){
                case 1:
                    this.eur += c.value;
                    break;
                case 2:
                    this.usd += c.value;
                    break;
                case 3:
                    this.gbp += c.value;
                    break;
                case 4:
                    this.cnh += c.value;
                    break;
                case 5:
                    this.jpy += c.value;
                    break;
                case 6:
                    this.ada += c.value;
                    break;
                case 7:
                    this.btc += c.value;
                    break;
            }
        }

        public Coin takeCoin(int type,float value){
            Coin c = new Coin(type);
            switch(type){
                case 1:
                    if(this.eur >= value){
                        c.value = value;
                        this.eur -= value;
                    }
                    break;
                case 2:
                    if(this.usd >= value){
                        c = new USD(value);
                        this.usd -= value;
                    }
                    break;
                case 3:
                    if(this.gbp >= value){
                        c = new GBP(value);
                        this.gbp -= value;
                    }
                    break;
                case 4:
                    if(this.cnh >= value){
                        c = new CNH(value);
                        this.cnh -= value;
                    }
                    break;
                case 5:
                    if(this.jpy >= value){
                        c = new JPY(value);
                        this.jpy -= value;
                    }   
                    break;
                case 6:
                    if(this.ada >= value){
                        c = new ADA(value);
                        this.ada -= value;
                    }
                    break;
                case 7:
                    if(this.btc >= value){
                        c = new BTC(value);
                        this.btc -= value;
                    }
                    break;
                default:
                    break;
                return c;
            }
        }
    }
}