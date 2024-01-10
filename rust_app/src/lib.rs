extern crate rand;


use std::os::raw::{c_void};

#[repr(C)]
pub struct ArrowSample {
    data: [[i32; 2]; 2],
}

impl ArrowSample {
    fn new() -> ArrowSample {
        ArrowSample {
            data: [[rand::random(), rand::random()],
                   [rand::random(), rand::random()]],
        }
    }
}

pub struct ArrowIterator {
    current: usize,
    data: Vec<ArrowSample>,
}

impl Iterator for ArrowIterator {
    type Item = *const ArrowSample;

    fn next(&mut self) -> Option<Self::Item> {
        if self.current >= self.data.len() {
            None
        } else {
            let reference = &self.data[self.current] as *const ArrowSample;
            self.current += 1;
            Some(reference)
        }
    }
}

#[no_mangle]
pub extern "C" fn create_arrow_iterator() -> *mut c_void {
    let iterator = Box::new(ArrowIterator {
        current: 0,
        data: vec![ArrowSample::new(), ArrowSample::new(), ArrowSample::new()],
    });

    Box::into_raw(iterator) as *mut c_void
}

#[no_mangle]
pub extern "C" fn next_arrow(iterator: *mut c_void) -> *const ArrowSample {
    let iterator = unsafe {
        assert!(!iterator.is_null());
        &mut *(iterator as *mut ArrowIterator)
    };

    iterator.next().unwrap_or(std::ptr::null())
}

#[no_mangle]
pub extern "C" fn free_arrow_iterator(iterator: *mut c_void) {
    if !iterator.is_null() {
        unsafe {
            let _ = Box::from_raw(iterator as *mut ArrowIterator);
        }
    }
}

// fn main() {
//     let mut arrow_iterator = ArrowIterator {
//         current: 0,
//         data: vec![ArrowSample::new(), ArrowSample::new(), ArrowSample::new()],
//     };

//     while let Some(sample_ptr) = arrow_iterator.next() {
//         let sample = unsafe { &*sample_ptr };
//         println!("ArrowSample: [[{}, {}], [{}, {}]]", 
//                  sample.data[0][0], sample.data[0][1],
//                  sample.data[1][0], sample.data[1][1]);
//     }
// }
