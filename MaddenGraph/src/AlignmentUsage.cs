// Really just getting some ideas down, this won't even compile.

// A freshly-constructed alignment contains a single player -- the
// snapper -- and is not legal.
// XXX: Is this a "player" or a "position"?  Need better domain term
var baseAlign = new Alignment();
Assert.That(baseAlign.Players.Length, Is.EqualTo(1));
Assert.That(baseAlign.Players[0], Is.EqualTo(baseAlign.Snapper));
Assert.That(baseAlign.IsLegal, Is.False);

// Adding a player on the line of scrimmage does stuff
// Still using the "player" noun
var twoPlayerAlign = baseAlign.AddPlayer(-15);
Assert.That(twoPlayerAlign.Players[0].x, // yards from ball
            Is.EqualTo(twoPlayerAlign.Players[1].x + 15);
Assert.That(twoPlayerAlign.PlayersOnLOS, Is.EqualTo(2));
Assert.That(twoPlayerAlign.Players[1],
            Is.EqualTo(twoPlayerAlign.EmlosLeft)); // interface?!
