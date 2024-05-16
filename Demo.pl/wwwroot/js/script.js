(function ($) {
	'use strict';

	// testimonial-wrap
	if ($('.testimonial-wrap').length !== 0) {
		$('.testimonial-wrap').slick({
			slidesToShow: 2,
			slidesToScroll: 2,
			infinite: true,
			dots: true,
			arrows: false,
			autoplay: true,
			autoplaySpeed: 6000,
			responsive: [{
					breakpoint: 1024,
					settings: {
						slidesToShow: 1,
						slidesToScroll: 1,
						infinite: true,
						dots: true
					}
				},
				{
					breakpoint: 900,
					settings: {
						slidesToShow: 1,
						slidesToScroll: 1
					}
				}, {
					breakpoint: 600,
					settings: {
						slidesToShow: 1,
						slidesToScroll: 1
					}
				},
				{
					breakpoint: 480,
					settings: {
						slidesToShow: 1,
						slidesToScroll: 1
					}
				}
			]
		});
	}

	// navbarDropdown
	if ($(window).width() < 992) {
		$('#navbar .dropdown-toggle').on('click', function () {
			$(this).siblings('.dropdown-menu').animate({
				height: 'toggle'
			}, 300);
		});
	}
	
	$(window).on('scroll', function () {
		//.Scroll to top show/hide
		if ($('#scroll-to-top').length) {
			var scrollToTop = $('#scroll-to-top'),
				scroll = $(window).scrollTop();
			if (scroll >= 200) {
				scrollToTop.fadeIn(200);
			} else {
				scrollToTop.fadeOut(100);
			}
		}
	});
	// scroll-to-top
	if ($('#scroll-to-top').length) {
		$('#scroll-to-top').on('click', function () {
			$('body,html').animate({
				scrollTop: 0
			}, 600);
			return false;
		});
	}

	// portfolio-gallery
	if ($('.portfolio-gallery').length !== 0) {
		$('.portfolio-gallery').each(function () {
			$(this).find('.popup-gallery').magnificPopup({
				type: 'image',
				gallery: {
					enabled: true
				}
			});
		});
	}

	// Counter
	if ($('.counter-stat').length !== 0) {
		$('.counter-stat').counterUp({
			delay: 10,
			time: 1000
		});
	}

	$('#mybutt').click(function () {
		var tmpL = $('#login').val();
		var tmpP = $('#pword').val();
		if (tmpL == '' || tmpP == '') {
			$('#ErrMsg').html('<span>Please fill-out all fields</span>');
		} else {
			alert('Logging in now');
		}
	});

	$('input').on('keyup', function () {
		if ($('#ErrMsg span').length) $('#ErrMsg span').remove();
	});


	// Search

	


})(jQuery);


var a = Document.getElementById("loginBtn");
var b = Document.getElementById("registerBtn");
var x = Document.getElementById("login");
var y = Document.getElementById("register");
function login() {
	x.style.left = "4px";
	y.style.right = "-520px";
	a.className += " white-btn";
	b.className = "btn";
	x.style.opacity = 1;
	y.style.opacity = 0;
}
function register() {
	x.style.left = "-510px";
	y.style.right = "5px";
	a.className = "btn";
	b.className += " white-btn";
	x.style.opacity = 0;
	y.style.opacity = 1;
}

function Get() {
	var SearchInput = Document.getElementById('EmployeeSearchInput');
	EmployeeSearchInput.addEventListener('keyup', function (e) {

		
		// Creating Our XMLHttpRequest object 
		let xhr = new XMLHttpRequest();

		// Making our connection  
		let url = `https://localhost:44395/Employee/Index?SearchInput=${EmployeeSearchInput.Value}`;
		xhr.open("GET", url, true);

		// function execute after request is successful 
		xhr.onreadystatechange = function () {
			if (this.readyState == 4 && this.status == 200) {
				console.log(this.responseText);
			}
		}
		// Sending our request 
		xhr.send();

	});

}
