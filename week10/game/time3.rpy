

label time3start:
    scene white
    "Did you know yourself back then?"
    menu:
        extend ""
        "yes":
            jump time3yes
        "no":
            jump time3no
    #me0 ""

    #jump time1end

label time3yes:

    jump time3end

label time3no:
    jump time3end
