VAR player_name = "player"
VAR player_class = "player"

#name ?????
Hello traveller.
#await text
What is your name?

-> name_input

=== name_input ===

->post_name

= post_name
Pleasure to meet you, {player_name}.
What is your school of training?
-> class_select

=== class_select ===
*[Warrior]
~player_class = "warrior"
-> warrior
*[Rogue]
~player_class = "rogue"
-> rogue
*[Mage]
~player_class = "mage"
-> mage

= warrior
Ah, another {player_class} come to test their metal. You will fit right in here.
-> post_class

= rogue
Ah, another fellow to skulk in the shadows. There are plenty of {player_class}s around.
-> post_class

= mage
Ah, another person to blow up farmland. Great.
-> post_class

=== post_class ===
Well I suppose I should thank you for your generous offering. May the city treat you well.
-> stabbing

=== stabbing ===
#name null
You feel a sharp pain in your sides.
You look down to see a dagger plunged into your abdomen.
Everything fades to black.
    -> END
