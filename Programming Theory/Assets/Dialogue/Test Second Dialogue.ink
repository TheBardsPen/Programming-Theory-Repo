VAR player_name = "player"
VAR player_class = "class"
VAR relationship = 0
VAR isQuestActive = false
-> begin

=== begin ===
#name Howard null
Hello, this is another test of dialogue.
I just want to make sure you can switch scripts, and keep NPC relations updated properly.
As you may have noticed, I (hopefully) don't have a last name, meaning the script worked.
Is that the case?
+[Yes] -> continue
+[No] -> fixme

=== continue ===
Great! Then that means the script is almost completed and ready to implement! -> END

=== fixme ===
Oh no! Then I guess you have some more work to do.

    -> END
