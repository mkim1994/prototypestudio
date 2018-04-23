# The script of the game goes in this file.

# Declare characters used by this game. The color argument colorizes the
# name of the character.



label start:    
    $ timechoice = None
    $ chosen1 = False
    $ chosen2 = False
    $ chosen3 = False

    scene white

    #show me0 neutral

    #me0 "Growing up, I used to think this:"
    jump choosetime

label choosetime:
    scene white with middissolve
    call screen timeline123
    # if not chosen1 and not chosen2 and not chosen3:
    #     call screen timeline123
    # elif not chosen1 and not chosen2 and chosen3:
    #     call screen timeline12
    # elif not chosen1 and chosen2 and chosen3:
    #     call screen timeline1
    # elif chosen1 and not chosen2 and chosen3:
    #     call screen timeline2
    # elif chosen1 and chosen2 and not chosen3:
    #     call screen timeline3
    # elif chosen1 and chosen2 and chosen3:
    #     jump aftereverything
    # elif chosen1 and not chosen2 and not chosen3:
    #     call screen timeline23
    # elif not chosen1 and chosen2 and not chosen3:
    #     call screen timeline13


label aftereverything:
    me0 "after everything"
    return

label afterchoice:
    if timechoice == "age 15":
        jump time1
    if timechoice == "age 17":
        jump time2
    if timechoice == "age 20":
        jump time3

label time1:
    $ chosen1 = True
    jump time1start


label time1end:
    jump choosetime  


label time2:
    $ chosen2 = True
    jump time2start

label time2end:
    me2 "hm"
    jump choosetime



label time3:
    $ chosen3 = True
    jump time3start

label time3end:
    me2 "hm"
    jump choosetime

label end:
    return
