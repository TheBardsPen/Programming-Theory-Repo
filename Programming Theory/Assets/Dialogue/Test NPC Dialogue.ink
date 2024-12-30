VAR player_name = "player"
VAR player_class = "warrior"
VAR relationship = 0
VAR isQuestActive = false
-> begin

=== begin ===
#name Test NPC
{ relationship:
 -0: You're new here.
 -else: Good to see you again.
}
-> options

=== options ===
How can I help you? 
+[Talk] -> talk_menu
+[Trade] -> trade_menu
+[Work] -> work_menu
+[Leave] -> goodbye

=== talk_menu ===
#relationship 1 null
{ relationship == 0: Hi, I'm Test NPC. Nice to meet you. You said your name was {player_name}?}
{ relationship > 0 and relationship < 5: { shuffle:
-Lovely weather we're having.
-Nice to see you again. I hope we can keep having these chats.
-I never get bored of talking to you.
}}
{ relationship >= 5: You've maxed out my relationship! Good Job!}
~ relationship += 1
-> options

=== trade_menu ===
{ relationship < 5: Oh, you think I want to trade with you? Maybe if I knew you better...}
{ relationship > 4: I would, but I haven't been programmed to do that yet.}
-> options
=== work_menu ===
Sorry bud, I haven't been programmed to do that yet.
-> options

=== goodbye ===
So long! Nice talking to you!

    -> END