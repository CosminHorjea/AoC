fn shoot(mut x_vel: i32, mut y_vel: i32, x1: i32, x2: i32, y1: i32, y2: i32) -> bool {
    let mut x = 0;
    let mut y = 0;
    let mut max_y = 0;
    while (x <= x2 && y >= y1) {
        if (x >= x1 && x <= x2 && y >= y1 && y <= y2) {
            // println!("Hit at {} {}", x, y);
            // println!("Max y : {}", max_y);
            return true;
        }
        x += x_vel;
        if x_vel > 0 {
            x_vel -= 1;
        }
        y += y_vel;
        max_y = y.max(max_y);
        y_vel -= 1;
    }
    false
}
fn main() {
    // target area: x=124..174, y=-123..-86
    //target area: x=20..30, y=-10..-5
    let content = include_str!("../day17.in");
    let coords = content.split_once(": ").unwrap().1;
    let x_coords = &coords.split_once(", ").unwrap().0[2..];
    let y_coords = &coords.split_once(", ").unwrap().1[2..];
    println!("{} {}", x_coords, y_coords);
    let x1 = x_coords.split_once("..").unwrap().0.parse::<i32>().unwrap();
    let x2 = x_coords.split_once("..").unwrap().1.parse::<i32>().unwrap();
    let y1 = y_coords.split_once("..").unwrap().0.parse::<i32>().unwrap();
    let y2 = y_coords.split_once("..").unwrap().1.parse::<i32>().unwrap();
    let mut positions = 0;
    // for x in 0..100 {
    //     for y in -100..100 {
    //         if (shoot(x, y, x1, x2, y1, y2)) {
    //             positions += 1;
    //         }
    //     }
    // }
    // for y in 11..1000 {
    //     if (shoot(16, y, x1, x2, y1, y2)) {
    //         positions += 1;
    //     }
    // }
    //wow, i didn't expect this to work,
    // pick an x that gives  the sum of first x numbers between x input, in this case 16*17 /2 = 136,
    // then search the y, it will converge
    for x in 0..180 {
        for y in -140..1000 {
            if (shoot(x, y, x1, x2, y1, y2)) {
                positions += 1;
            }
        }
    } // ok, this is hacky as hell, but x can only be withing the target, and i think i cand cut down the y more, but hey, 2 gold stars
    println!("positions: {}", positions);
}
