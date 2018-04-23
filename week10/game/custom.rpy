init -1:
    transform fadein:
        alpha 0.0
        easein 1.0 alpha 1.0

init offset = -1

define fastdissolve = Dissolve(.1)
define middissolve = Dissolve(1)
define slowdissolve = Dissolve(3)
define enddissolve = Dissolve(5)


define me0 = Character("me")
define me1 = Character("me")
define me2 = Character("me")
define me3 = Character("me")

define dark = Character("",what_color="#ffffff",window_background="textboxdark.png")


image black = "#000000"
image white = "#ffffff"


screen timeline123:
    imagebutton auto "time1_%s.png" focus_mask True action [SetVariable("timechoice","age 15"), Jump('afterchoice')] at fadein
    imagebutton idle "time2_hover.png"
    imagebutton idle "time3_hover.png"

screen timeline23:
    imagebutton idle "time1_idle.png"
    imagebutton auto "time2_%s.png" focus_mask True action [SetVariable("timechoice","age 17"), Jump('afterchoice')]
    imagebutton auto "time3_%s.png" focus_mask True action [SetVariable("timechoice","age 20"), Jump('afterchoice')]

screen timeline1:
    imagebutton auto "time1_%s.png" focus_mask True action [SetVariable("timechoice","age 15"), Jump('afterchoice')]
    imagebutton idle "time2_idle.png"
    imagebutton idle "time3_idle.png"


screen timeline2:
    imagebutton idle "time1_idle.png"
    imagebutton auto "time2_%s.png" focus_mask True action [SetVariable("timechoice","age 17"), Jump('afterchoice')]
    imagebutton idle "time3_idle.png"

screen timeline3:
    imagebutton idle "time1_idle.png"
    imagebutton idle "time2_idle.png"
    imagebutton auto "time3_%s.png" focus_mask True action [SetVariable("timechoice","age 20"), Jump('afterchoice')]

screen timeline12:
    imagebutton auto "time1_%s.png" focus_mask True action [SetVariable("timechoice","age 15"), Jump('afterchoice')]
    imagebutton auto "time2_%s.png" focus_mask True action [SetVariable("timechoice","age 17"), Jump('afterchoice')]
    imagebutton idle "time3_idle.png"


screen timeline13:
    imagebutton auto "time1_%s.png" focus_mask True action [SetVariable("timechoice","age 15"), Jump('afterchoice')]
    imagebutton idle "time2_idle.png"
    imagebutton auto "time3_%s.png" focus_mask True action [SetVariable("timechoice","age 20"), Jump('afterchoice')]











