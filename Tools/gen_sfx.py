import wave, struct, math, random, os
SR = 44100
out = "Assets/Audio"

def env(t, dur, attack=0.005):  # fast attack, linear decay
    return min(t/attack, 1.0) * max(0.0, 1.0 - t/dur)

def render(name, dur, fn, vol=0.8):
    n = int(SR*dur); frames = bytearray()
    for i in range(n):
        t = i/SR
        s = max(-1.0, min(1.0, fn(t) * env(t, dur) * vol))
        frames += struct.pack('<h', int(s*32767))
    with wave.open(f"{out}/{name}.wav", 'wb') as w:
        w.setnchannels(1); w.setsampwidth(2); w.setframerate(SR); w.writeframes(frames)
    print("wrote", name)

def square(f, t): return 1.0 if math.sin(2*math.pi*f*t) > 0 else -1.0
def slide(f0, f1, dur, t): return f0 + (f1-f0)*(t/dur)
random.seed(1)
noise = lambda: random.uniform(-1, 1)

# hit: short low thud + noise
render("hit", 0.08, lambda t: 0.6*math.sin(2*math.pi*slide(180, 60, 0.08, t)*t) + 0.4*noise(), 0.7)
# death: noise burst with pitch-dropping square
render("death", 0.3, lambda t: 0.5*square(slide(400, 40, 0.3, t), t) + 0.5*noise(), 0.6)
# pickup: rising two-note blip
render("pickup", 0.14, lambda t: square(660 if t < 0.07 else 990, t), 0.35)
# levelUp: ascending arpeggio C E G C
def arp(t):
    notes = [523, 659, 784, 1047]; i = min(int(t/0.15), 3)
    return square(notes[i], t) * (1.0 - (t % 0.15)/0.15*0.5)
render("levelUp", 0.6, arp, 0.4)
# hurt: buzzy low pitch drop
render("hurt", 0.25, lambda t: 0.7*square(slide(220, 80, 0.25, t), t) + 0.3*noise(), 0.6)
# click: tiny tick
render("click", 0.04, lambda t: square(1200, t), 0.3)
# gameOver: descending melody
def go(t):
    notes = [523, 440, 349, 262]; i = min(int(t/0.35), 3)
    return 0.5*square(notes[i], t) + 0.5*math.sin(2*math.pi*notes[i]/2*t)
render("gameOver", 1.4, go, 0.4)
# shoot: short laser pew, pitch drop
render("shoot", 0.1, lambda t: square(slide(900, 400, 0.1, t), t), 0.3)
# zap: crackly lightning
render("zap", 0.18, lambda t: 0.5*square(slide(1400, 200, 0.18, t), t) + 0.5*noise(), 0.4)
