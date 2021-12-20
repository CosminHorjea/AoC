
#[derive(Debug)]

struct opPacket {
    version: u8,
    typeLabel: u8,
    lengthType: bool,
    length: u16,
    num_packets: u16,
    subpackets: String
}
#[derive(Debug)]
struct litPacket {
    version: u8,
    typeLabel: u8,
    literalValue: u32,
}


fn convert_to_binary_from_hex(hex: &str) -> String {
    hex.chars().map(to_binary).collect()
}

fn to_binary(c: char) -> &'static str {
    match c {
        '0' => "0000",
        '1' => "0001",
        '2' => "0010",
        '3' => "0011",
        '4' => "0100",
        '5' => "0101",
        '6' => "0110",
        '7' => "0111",
        '8' => "1000",
        '9' => "1001",
        'A' => "1010",
        'B' => "1011",
        'C' => "1100",
        'D' => "1101",
        'E' => "1110",
        'F' => "1111",
        _ => "",
    }
}


fn parseLitPacket(bin: &str)-> (litPacket,u32){
    let mut verStr = bin.chars().take(3).collect::<String>();
    let ver = u8::from_str_radix(&verStr, 2).unwrap();
    let typeStr = bin.chars().skip(3).take(3).collect::<String>();
    let typeLabel = u8::from_str_radix(&typeStr, 2).unwrap();
    let mut full_number: Vec<String> = Vec::new();
    let mut parsed = 6;
    for bits in bin.chars().skip(6).collect::<Vec<char>>().chunks(5) {
        if bits.len()<5 {
            continue;
        }
        // println!("{}", bits.iter().collect::<String>());
        full_number.push(bits[1..5].iter().collect::<String>());
        parsed+=5;
        if(bits[0]=='0'){
            break;
        }
        
    }
    let num = u32::from_str_radix(&full_number.join(""),2).unwrap();

    (litPacket {
        version:ver,
        typeLabel: typeLabel,
        literalValue: num,
    }, parsed)
}

fn parseOpPacket(bin: &str)-> (opPacket,u32){
    let mut verStr = bin.chars().take(3).collect::<String>();
    let ver = u8::from_str_radix(&verStr, 2).unwrap();
    let typeStr = bin.chars().skip(3).take(3).collect::<String>();
    let typeLabel = u8::from_str_radix(&typeStr, 2).unwrap();
    let lenType = bin.chars().skip(6).take(1).collect::<Vec<char>>()[0];
    let mut length = 0;
    let mut num_packets = 0;
    let mut subPackets:String = String::new();
    let mut parsed = 7;
    match &lenType {
        '0'=>{
            let strlength = bin.chars().skip(7).take(15).collect::<String>();
            length = u16::from_str_radix(&strlength, 2).unwrap();
            parsed += 15;
            subPackets = bin.chars().skip(22).take(length as usize).collect::<String>();
        },
        '1'=>{
            let strlength = bin.chars().skip(7).take(11).collect::<String>();
            parsed += 11;
            num_packets = u16::from_str_radix(&strlength, 2).unwrap();
        },
        _=>{
            panic!("AAAAAAAAAAAAAAAAAAAAAAAAA");
        }
    }
    (opPacket{
        version: ver,
        typeLabel: typeLabel,
        lengthType: if lenType == '0' {false} else {true},
        length: length,
        subpackets: subPackets,
        num_packets: num_packets,
    },parsed)
}

fn getPacketType(bin: &str)->u8 {
    let typeStr = bin.chars().skip(3).take(3).collect::<String>();
    let typeLabel = u8::from_str_radix(&typeStr, 2).unwrap();
    typeLabel
}

fn main() {
    let content = include_str!("../day16.in");
    let input = content.lines().next().unwrap();
    let result = convert_to_binary_from_hex(input);
    let mut version_sum =0;
    let rs = parseOpPacket(&result);
    println!("{:?}",rs);
    let rs2 = parseOpPacket(&rs.0.subpackets);
    println!("{:?}",rs2);
    let rs3 = parseOpPacket(&rs2.0.subpackets);
    println!("{:?}",rs3);
    
}
