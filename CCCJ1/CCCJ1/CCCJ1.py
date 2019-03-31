in1 = input()
in2 = input()
in3 = input()
in4 = input()

result = True

if not (in1 == "8" or in1 == "9") or not (in2 == in3) or not (in4 == "8" or in4 == "9"):
    result = False

if result == False:
    print("answer")
else:
    print("ignore")
    