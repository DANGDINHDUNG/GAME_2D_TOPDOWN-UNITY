-> main

=== guide ===
How are you feeling today?
+ [Happy]
    That makes me feel happy as well #portrait:lilly_happy
+ [Sad]
    Oh, well that makes me sad too. #portrait:lilly_sad
    
- Don't trust him, he's <b>not</b> a real doctor! #speaker:Test #portrait:alexia_neutral

Well, do you have any more questions? #speaker:Lilly #portrait:lilly_neutral
+ [Yes]
    -> guide
+ [No]
    Goodbye, then!
    -> END
    
=== main ===
Thank you for helping me #speaker:Lilly #portrait:lilly_happy
Your outfit looks weird, aren't you from here? #speaker:Lilly #portrait:lilly_neutral
+ [That's right! I come from a far away place.]
     You're lucky you're here, otherwise I don't know what I'll do #speaker:Lilly #portrait:lilly_happy
     + + [Why would someone like you appear in this dangerous place?]
     I plan to come up here to pick some herbs for medicine. But unexpectedly, I encountered these monsters in the middle of the road. #speaker:Lilly #portrait:lilly_sad
     Fortunately, you came to help in time #speaker:Lilly #portrait:lilly_happy
     If it ok, can you come with me to the village so I can thank you! #speaker:Lilly #portrait:lilly_neutral
         +++ [In that case, would you mind taking me to the village, I'm new here so I'm not familiar with the place.]
             If so, great! You can wait for me to finish picking the medicine and then you can go home! together! #speaker:Lilly #portrait:lilly_happy
             -> DONE
-> END