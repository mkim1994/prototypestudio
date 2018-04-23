


label time2start:
    scene white
    "Did you know yourself back then?"
    menu:
        extend ""
        "yes":
            jump time2yes
        "no":
            jump time2no
    #me0 ""

    #jump time1end

label time2yes:
    "yes"
    jump time2end

label time2no:
    "no"
    jump time2end
