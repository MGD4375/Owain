//  load the file

public class Card
{

    private Card() { }

    public Card(string data)
    {
        Value = data[0] switch
        {
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            'T' => 10,
            'J' => 11,
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            _ => throw new Exception("Could not map value from name")
        };


        Name = data[0];
        Suit = data[1];
    }

    public int Value { get; set; } // Terrible thing to do, but it's quick! 
    public char Name { get; private set; }
    public char Suit { get; private set; }
}




//  for each line
//  get value of left 
//  get value of right
//  compare hands
//  record winner 