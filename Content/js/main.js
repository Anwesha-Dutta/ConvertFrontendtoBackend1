
    $('.slider').owlCarousel({
        loop:false,
        margin:10,
        nav:true,
        responsive:{
            0:{
                items:1
            },
            600:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });

$('.slider2').owlCarousel({
    loop:true,
    margin:25,
    nav:false,
    autoplay:true,
    autoplayTimeout:3000,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:2
        },
        1000:{
            items:3
        }
    }
})

$('.slider3').owlCarousel({
    loop:true,
    margin:0,
    nav:true,
    autoplay:false,
    responsive:{
        0:{
            items:2
        },
        400:{
            items:3
        },
        
        
        600:{
            items:4
        },
        1000:{
            items:6
        }
    }
})

$('.slider4').owlCarousel({
    loop:true,
    margin:25,
    nav:false,
    autoplay:false,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:1
        }
    }
})


function play()
{
    // document.getElementById("video");
    document.querySelector(".video").style.display="block";
}

function cut()
{
    document.querySelector(".video").style.display="none";

}