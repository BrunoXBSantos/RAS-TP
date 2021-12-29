<template>
  <div>
    <DefaultNavbar />
    <div id="content">
      <div class="card" id="homecard">
        <div class="card-image">
          <carousel>
            <carousel-slide
              v-for="slide in slides"
              :key="slide"
              class="carousel-slider"
            >
              <img :src="slide" :alt="slide" />
            </carousel-slide>
          </carousel>
        </div>

        <div class="card-content">
          <div class="content has-text-justified">
            <p>
              A RASBet é uma Plataforma de Apostas. RASBet é uma Plataforma de
              Apostas. RASBet é uma Plataforma de Apostas. RASBet é uma
              Plataforma de Apostas. RASBet é uma Plataforma de Apostas. RASBet
              é uma Plataforma de Apostas. RASBet é uma Plataforma de Apostas.
              RASBet é uma Plataforma de Apostas. RASBet é uma Plataforma de
              Apostas. RASBet é uma Plataforma de Apostas. RASBet é uma
              Plataforma de Apostas. RASBet é uma Plataforma de Apostas.
            </p>
          </div>

          <div class="buttons is-centered">
            <a class="button is-light" href="/login"> Login </a>
            <a class="button is-danger" href="/register">
              <strong>Register</strong>
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DefaultNavbar from '@/components/DefaultNavbar'
import Carousel from '@/components/Carousel.vue'
import CarouselSlide from '@/components/CarouselSlide.vue'

export default {
  data () {
    return {
      slides: ['1.jpg', '2.jpg', '3.jpg', '4.jpg', '5.jpg']
    }
  },
  name: 'Home',
  components: {
    DefaultNavbar,
    Carousel: Carousel,
    CarouselSlide: CarouselSlide
  }
}
</script>

<style scoped>
#content {
  margin: 8% auto;
  width: 60%;
}

@media (max-width: 770px) {
  #homecard {
    margin: 3% auto;
    min-height: 80vh;
  }
}

.carousel {
  position: relative;
  overflow: hidden;
  max-width: 100%;
  height: 400px;
  z-index: 10;
}

img {
  object-fit: contain;
}

.carousel-slider {
  position: absolute;
  top: 10;
  left: 0;
  bottom: 0;
  right: 0;
}

.carousel-slider img {
  width: 100%;
  height: 100%;
}
</style>
