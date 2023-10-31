using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    int _score; 
    public int getScore() 
    { 
        return _score; 
    }

    public void incrementScore() 
    {
        _score++;
    }

    public void reiniciar()
    {
        _score = 0;
    }


}
