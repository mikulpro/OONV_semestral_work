namespace Semestralni_prace;

/*public class observer_example
{
    
    
    
    public class Player : Subject
    {
        public event EventHandler LevelUp;

        private int level;

        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                OnLevelUp();
            }
        }

        protected void OnLevelUp()
        {
            LevelUp?.Invoke(this, EventArgs.Empty);
        }
    }

    public interface Observer
    {
        void HandleLevelUp(object sender, EventArgs e);
    }

    public class GUI : Observer
    {
        public void HandleLevelUp(object sender, EventArgs e)
        {
            Player player = (Player)sender;
            Console.WriteLine("Player has leveled up to level " + player.Level);
        }
    }
    }*/
