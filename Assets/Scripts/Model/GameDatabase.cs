using Platformer.Gameplay;
using UnityEngine;

namespace Platformer.Model
{
    public class GameDatabase
    {
        public static GameDatabase Instance = new GameDatabase();
        public UserData CurrentUser { get; private set; }

        public ParticleSystem particleSystem;

        private GameDatabase()
        {
            CurrentUser = new UserData();
            PlayerTokenCollision.OnExecute += PlayerCollectedToken;
            EnemyDeath.OnExecute += PlayerKilledEnemy;
            
        }

        private void PlayerKilledEnemy(EnemyDeath enemyDeath)
        {
            string enemyName = enemyDeath.enemy._collider.name;
            string enemyCategory = enemyName.Split('_')[0];

            if(particleSystem == null)
                particleSystem = GameObject.Find("ConfettiCelebration").GetComponent<ParticleSystem>();
            particleSystem.transform.position = enemyDeath.enemy._collider.transform.position;
            particleSystem.Play();

            switch (enemyCategory)
            {
                case "PurpleEnemy":
                    CurrentUser.EnemiesKilled += 2;
                    break;
                case "AlienEnemy":
                    CurrentUser.EnemiesKilled += 5;
                    break;                    
                default:
                    CurrentUser.EnemiesKilled += 1;
                    break;
            }            
        }

        private void PlayerCollectedToken(PlayerTokenCollision playerTokenCollision)
        {
            CurrentUser.Tokens++;
        }

        public void SetUsername(string newName)
        {
            CurrentUser.Username = newName;
        }

        public void ResetScore()
        {
            CurrentUser.Tokens = 0;
            CurrentUser.EnemiesKilled = 0;
        }

        ~GameDatabase()
        {
            PlayerTokenCollision.OnExecute -= PlayerCollectedToken;
            EnemyDeath.OnExecute -= PlayerKilledEnemy;
        }

        public class UserData
        {
            public string Username = "";
            public int Tokens { get; internal set; }
            public int EnemiesKilled { get; internal set; }
            public int Score => Tokens * 10 + EnemiesKilled * 100;            
        }
    }
}
   
