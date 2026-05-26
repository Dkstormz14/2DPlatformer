using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    //Character Damaged and damage received
    public static UnityAction<GameObject, int> characterDamaged;

    // Character Healed and amount of health restored
    public static UnityAction<GameObject, int> characterHealed;
}
