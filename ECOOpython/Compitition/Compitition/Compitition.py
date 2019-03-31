W = int(input())
H = int(input())
w = int(input())
h = int(input())
s = int(input())

avalible = [[], []]

for x in range(W):
    for f in range(H):
        avalible[f][x].append(False)

'''
for i in range(w):
    for n in range(h):
        avalible[i][n] = False
        avalible[W - i][H - n] = False
'''

print(avalible)
