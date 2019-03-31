N = int(input())
ln1 = input()
ln2 = input()

n1 = list(ln1)
n2 = list(ln2)

sameSpace = 0

for i in range(N):
    if n1[i] == "C" and n2[i] == "C":
        sameSpace += 1

print(sameSpace)
        