using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace RobotKakashisito
{
    class Kakashisito : JuniorRobot
    {


        bool pared = false;
        int cont = 0;


        public override void Run()
        {

            SetColors(BLUE, WHITE, RED, RED, RED); // body,gun,radar,bullet,scan
            // Secuencia principal
            while (true)
            {
                if (Others > 1)
                {
                    Ahead(100); // Mueve adelante 100
                    TurnGunRight(360); // gira el cañon 360Grados
                    Back(100); // Mueve atras 100
                    TurnGunRight(360); // gira el cañon 360Grados
                }
                else
                {
                    Ahead(300); // Mueve adelante 100
                    TurnGunRight(360); // gira el cañon 360Grados
                    Back(300); // Mueve atras 100
                    TurnGunRight(360); // gira el cañon 360Grados
                }
            }
        }

        #region funciones basicas

        // Ahead  --> se mueve adelante por pixeles
        // Back   --> se mueve atras en pixeles
        // TurnGunTo -->mueve el cañon a un angulo especifico
        // Fire  --> dispara con la potencia especificada
        // TurnTo --> mueve el tanque a un angulo especifico
        #endregion

        #region atributos basicos

        // Energy --> energia restante del tanque
        // Others --> total de enemigos
        // ScannedAngle --> angulo del ultimo tanque enemigo encontrado

        #endregion

        /*Funcion cuando golpean al robot*/
        public override void OnHitByBullet()
        {            

            cont++;
            if (cont == 4)
            {
                cont = 0;
                //		turnGunTo(hitByBulletAngle);
                //		fire(3);      			 	
                //		turnBackRight(80, 60 - hitByBulletBearing);//(distance,grados) mover el robot para que no me sigan dando, a partir del angulo del robot que me pego
                TurnBackRight(120, 130);//(distance,grados) mover el robot para que no me sigan dando, a partir del angulo del robot que me pego
                //     	turnGunTo(scannedAngle);  //mover el cañon en donde estaba mi enemigo

                if (pared == false)
                {  // si no me he chocado anteriormente con la pared mover hacia atras
                    Back(80);
                    pared = true;
                }
                else
                {
                    Ahead(80);  // si me choque seguido con la pared entonces muevalo hacia adelante
                    pared = false;
                }
            }

            if (Others > 1)
            {
                Fire(1);
            }
            else
            {
                if (Energy <= 50)
                {   //energia mi robot
                    Fire(1);  // disparar con energia 1
                }
                else
                {
                    if (Energy <= 80)
                    {
                        Fire(2);
                    }
                    else
                    {
                        Fire(3);
                    }
                }
            }
        }

        /*Cuando el robot encuentra a un enemigo*/
        public override void OnScannedRobot()
        {
         
            TurnGunTo(ScannedAngle);	//gira el cañon a la parte en donde encontro el enemigo, lo que permite un seguimiento constante de este
            

            if (Others > 1)
            {
                if (Energy <= 30)
                {   //energia mi robot
                    Fire(1);  // disparar con energia 1
                }
                else
                {
                    if (Energy <= 60)
                    {
                        Fire(2);
                    }
                    else
                    {
                        Fire(3);
                    }
                }
            }            
        }


        /*Cuando el robot golpea al enemigo*/
        public override void OnHitRobot()
        {
            if (cont > 1)
            {
                cont--;                
            }
        }


        /*Cuando encuentra una muralla*/
        public override void OnHitWall()
        {
            if (pared == false)
            {
                Back(250);
                pared = true;
            }
            else
            {
                Ahead(250);
                TurnAheadLeft(80, 60 - HitByBulletBearing);//distance,grados
                pared = false;
            }
        }

    }
}
