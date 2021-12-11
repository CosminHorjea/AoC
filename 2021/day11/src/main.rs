use std::collections::VecDeque;

fn main() {
    let content = include_str!("day11.in");
    let mut energy_levels = content
        .lines()
        .map(|line| {
            line.chars()
                .map(|chr| chr.to_digit(10).unwrap())
                .collect::<Vec<u32>>()
        })
        .collect::<Vec<Vec<u32>>>();
    let number_of_steps = 1000;
    let directions: Vec<(i32, i32)> = vec![
        (0, 1),
        (-1, 1),
        (-1, 0),
        (-1, -1),
        (0, -1),
        (1, -1),
        (1, 0),
        (1, 1),
    ];
    // for line in energy_levels.iter() {
    //     println!("{:?}", line);
    // }
    let mut total_flashes = 0;
    for iter in 0..number_of_steps {
        let mut copy_of_energy = energy_levels.clone();
        for i in 0i32..10 {
            for j in 0i32..10 {
                copy_of_energy[i as usize][j as usize] += 1;
            }
        }
        for i in 0i32..10 {
            for j in 0i32..10 {
                if copy_of_energy[i as usize][j as usize] == 0 {
                    continue;
                }

                if (copy_of_energy[i as usize][j as usize] > 9) {
                    let mut flashing: VecDeque<(i32, i32)> = VecDeque::new();
                    flashing.push_back((i, j));
                    while flashing.len() > 0 {
                        total_flashes += 1;
                        let current_pos = flashing.pop_front().unwrap();
                        copy_of_energy[current_pos.0 as usize][current_pos.1 as usize] = 0;
                        for dir in directions.iter() {
                            let new_i = current_pos.0 + dir.0;
                            let new_j = current_pos.1 + dir.1;
                            if (new_i >= 0 && new_i < 10) && (new_j >= 0 && new_j < 10) {
                                if (copy_of_energy[new_i as usize][new_j as usize] >= 9) {
                                    copy_of_energy[new_i as usize][new_j as usize] = 0;
                                    flashing.push_back((new_i as i32, new_j as i32));
                                } else if (copy_of_energy[new_i as usize][new_j as usize] != 0) {
                                    copy_of_energy[new_i as usize][new_j as usize] += 1;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (copy_of_energy
            .iter()
            .filter(|line| line.iter().all(|x| *x == 0))
            .count()
            == 10)
        {
            println!("Part 2 : Step: {}", iter + 1);
            break;
        }
        // for line in copy_of_energy.iter() {
        //     println!("{:?}", line);
        // }
        energy_levels = copy_of_energy.clone();
        // copy_of_energy.clear();
    }
    println!("Part1 {}", total_flashes);
}
//319 too low
