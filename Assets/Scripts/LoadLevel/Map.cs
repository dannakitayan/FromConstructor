using UnityEngine;

public class Map
{
    PAK rgm;

    public Map(PAK pak)
    {
        rgm = pak;
    }

    public GameObject GetObject(char letter) => letter switch
    {
        '' => rgm.Tex1,
        '' => rgm.Tex2,
        '' => rgm.Tex3,
        '' => rgm.Tex4,
        '' => rgm.Tex5,
        '' => rgm.Tex6,
        '' => rgm.Tex7,
        '' => rgm.Tex8,
        '' => rgm.Tex9,
        '' => rgm.Tex10,

        '' => rgm.Tex11,
        '' => rgm.Tex12,
        '' => rgm.Tex13,
        '' => rgm.Tex14,
        '' => rgm.Tex15,
        '' => rgm.Tex16,
        ' ' => rgm.Tex17,
        '!' => rgm.Tex18,
        '"' => rgm.Tex19,
        '#' => rgm.Tex20,

        '$' => rgm.Tex21,
        '%' => rgm.Tex22,
        '&' => rgm.Tex23,
        '\'' => rgm.Tex24,
        '(' => rgm.Tex25,
        ')' => rgm.Tex26,
        '*' => rgm.Tex27,
        '+' => rgm.Tex28,
        ',' => rgm.Tex29,
        '-' => rgm.Tex30,

        '.' => rgm.Tex31,
        '/' => rgm.Tex32,
        '0' => rgm.Tex33,
        '1' => rgm.Tex34,
        '2' => rgm.Tex35,
        '3' => rgm.Tex36,
        '4' => rgm.Tex37,
        '5' => rgm.Tex38,
        '6' => rgm.Tex39,
        '7' => rgm.Tex40,

        '@' => rgm.Sprite1,
        'A' => rgm.Sprite2,
        'B' => rgm.Sprite3,
        'C' => rgm.Sprite4,
        'D' => rgm.Sprite5,
        'E' => rgm.Sprite6,
        'F' => rgm.Sprite7,
        'G' => rgm.Sprite8,
        'H' => rgm.Sprite9,
        'I' => rgm.Sprite10,

        'X' => rgm.AmmoBox,
        'W' => rgm.Medkit,
        'Y' => rgm.Key,

        '9' => rgm.NextLevel,

        'Q' => rgm.Weapon1,
        'R' => rgm.Weapon2,
        'S' => rgm.Weapon3,
        'T' => rgm.Weapon4,
        'U' => rgm.Weapon5,
        'V' => rgm.Weapon6,

        'h' => rgm.Enemy1,
        'i' => rgm.Enemy2,
        'j' => rgm.Enemy3,
        'k' => rgm.Enemy4,
        'l' => rgm.Enemy5,

        '8' => rgm.Door,
        '\\' => rgm.Player,

        _ => rgm.Empty
    };
}
