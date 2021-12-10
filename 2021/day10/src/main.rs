fn get_complement(p: char) -> char {
    let closing = ")]}>";
    let opening = "([{<";
    if closing.contains(p) {
        return opening.chars().nth(closing.find(p).unwrap()).unwrap();
    } else {
        return closing.chars().nth(opening.find(p).unwrap()).unwrap();
    }
}
fn main() {
    let content = include_str!("../day10.in");
    let items: Vec<Vec<char>> = content.lines().map(|line| line.chars().collect()).collect();
    let opening = "([{<";

    // let mut total_points = 0;
    let mut scores: Vec<u128> = Vec::new();
    // for line in items {
    //     let mut parsed: Vec<char> = Vec::new();
    //     // let mut found_err = 0;
    //     for chr in line {
    //         if opening.contains(chr) {
    //             parsed.push(chr);
    //         } else {
    //             if (parsed.last().unwrap() == &get_complement(chr)) {
    //                 // println!("Found: {} and complement {}", parsed.last().unwrap(), chr);
    //                 parsed.pop();
    //             } else {
    //                 if chr == ')' {
    //                     total_points += 3;
    //                 } else if chr == ']' {
    //                     total_points += 57;
    //                 } else if chr == '}' {
    //                     total_points += 1197;
    //                 } else if chr == '>' {
    //                     total_points += 25137;
    //                 }
    //                 // found_err = 1;
    //                 break;
    //                 println!("{}\n", total_points)
    //             }
    //         }
    //     }
    // }
    // print!("Part 1 : {}, Powered by Rust", total_points);

    for line in items {
        let mut line_score = 0;
        let mut parsed: Vec<char> = Vec::new();
        let mut complete_line = 1;
        for chr in line {
            if opening.contains(chr) {
                parsed.push(chr);
            } else {
                if parsed.last().unwrap() == &get_complement(chr) {
                    parsed.pop();
                } else {
                    complete_line = 0;
                    break;
                }
            }
        }
        if complete_line == 1 {
            for par in parsed.iter().rev() {
                line_score *= 5;
                match get_complement(*par) {
                    ')' => {
                        line_score += 1;
                    }
                    ']' => {
                        line_score += 2;
                    }
                    '}' => {
                        line_score += 3;
                    }
                    '>' => {
                        line_score += 4;
                    }
                    _ => {
                        panic!("Your mum");
                    }
                }
            }
            scores.push(line_score);
        }
    }
    scores.sort();
    println!(
        "Part 2: {}, powered by RustLang",
        scores.iter().nth(scores.len() / 2).unwrap()
    )
}
