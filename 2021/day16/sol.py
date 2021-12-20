# Not my solution, but i wanted to work out on this example
# https://github.com/mebeim/aoc/tree/master/2021#day-16---packet-decoder
class BitStream:
    def __init__(self, file):
        hexdata = file.read()
        rawdata = bytes.fromhex(hexdata)

        self.pos = 0
        self.bits = ""

        for byte in rawdata:
            self.bits += "{:08b}".format(byte)

    def decode_int(self, nbits):
        res = int(self.bits[self.pos : self.pos + nbits], 2)
        self.pos += nbits
        return res

    def decode_one_packet(self):
        version = self.decode_int(3)
        tid = self.decode_int(3)
        data = self.decode_packet_data(tid)
        return (version, tid, data)

    def decode_value_data(self):
        value = 0
        group = 0b10000

        while group & 0b10000:
            group = self.decode_int(5)
            value <<= 4
            value += group & 0b1111

        return value

    def decode_n_packets(self, n):
        return [self.decode_one_packet() for _ in range(n)]

    def decode_len_packets(self, length):
        end = self.pos + length
        pkts = []

        while self.pos < end:
            pkts.append(self.decode_one_packet())

        return pkts

    def decode_operator_data(self):
        ltid = self.decode_int(1)

        if ltid == 1:
            return self.decode_n_packets(self.decode_int(11))

        return self.decode_len_packets(self.decode_int(15))

    def decode_packet_data(self, tid):
        if tid == 4:
            return self.decode_value_data()
        return self.decode_operator_data()


def sum_versions(packet):
    v, tid, data = packet

    if tid == 4:
        return v

    return v + sum(map(sum_versions, data))


from math import prod


def evaluate(packet):
    _, tid, data = packet

    if tid == 4:
        return data

    values = map(evaluate, data)

    if tid == 0:
        return sum(values)
    if tid == 1:
        return prod(values)
    if tid == 2:
        return min(values)
    if tid == 3:
        return max(values)

    a, b = values

    if tid == 5:
        return int(a > b)
    if tid == 6:
        return int(a < b)
    return int(a == b)  # tid == 7


fin = open("day16.in", "r")

stream = BitStream(fin)

packet = stream.decode_one_packet()
v_sum = sum_versions(packet)

print(v_sum)
print(evaluate(packet))
