<template>
  <div class="max-w-7xl mx-auto">
    <header class="mb-10">
      <h1 class="text-3xl font-extrabold tracking-tight mb-2">Welcome back, Elena.</h1>
      <p class="text-gray-600 dark:text-gray-400 text-md">Your reading journey continues. You have 2 books due this week.</p>
    </header>

    <!-- Bento Grid Layout -->
    <div class="grid grid-cols-1 md:grid-cols-12 gap-6">
      <!-- User Stats Bento Items -->
      <div class="md:col-span-8 grid grid-cols-1 sm:grid-cols-3 gap-6">
        <UCard class="border-l-4 border-l-primary-500 shadow-sm architect-shadow">
          <p class="text-gray-500 dark:text-gray-400 mb-4 uppercase tracking-widest text-[10px] font-bold">Books Read</p>
          <div class="flex items-baseline gap-2">
            <span class="text-3xl font-black">128</span>
            <span class="text-green-600 dark:text-green-400 text-xs font-bold flex items-center">
              <UIcon name="i-lucide-trending-up" class="mr-1" /> +4
            </span>
          </div>
        </UCard>
        
        <UCard class="shadow-sm architect-shadow">
          <p class="text-gray-500 dark:text-gray-400 mb-4 uppercase tracking-widest text-[10px] font-bold">Active Holds</p>
          <div class="flex items-baseline gap-2">
            <span class="text-3xl font-black">03</span>
            <span class="text-gray-500 dark:text-gray-400 text-xs font-medium">In Queue</span>
          </div>
        </UCard>

        <UCard class="shadow-sm architect-shadow">
          <p class="text-gray-500 dark:text-gray-400 mb-4 uppercase tracking-widest text-[10px] font-bold">Member Points</p>
          <div class="flex items-baseline gap-2">
            <span class="text-3xl font-black">2,450</span>
            <UIcon name="i-lucide-star" class="text-yellow-500 text-xl" />
          </div>
        </UCard>
      </div>

      <!-- Side Reservation Queue -->
      <div class="md:col-span-4 md:row-span-2 relative">
        <UCard class="h-full bg-gray-100 dark:bg-gray-800 ring-0 architect-shadow border border-gray-200 dark:border-gray-700">
          <div class="flex justify-between items-center mb-6">
            <h2 class="text-sm font-bold uppercase tracking-tight">Reservation Queue</h2>
            <UBadge color="primary" variant="subtle" size="xs">3 PENDING</UBadge>
          </div>
          <div class="space-y-6">
            
            <div class="flex gap-4">
              <div class="w-12 h-16 bg-gray-200 dark:bg-gray-700 rounded overflow-hidden flex-shrink-0">
                <img alt="The Midnight Library" class="w-full h-full object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuASv8jiAGmPLXQF-sb7UxA0ii-kE3yV6gW6PiRQCIIFbtidKhdsie9e-kzCLhze1db-rg_hVTXwkSXYmzr9sW8HaHfZWVZErTYvuCYz_0jhR2G3Ulk3Zm4PvcMEmAuI1MEPZbfM-P8tTj7kE1MBq4PeqcbhP0y-D86_WmYRMezEPCTGXAO-AWzMa1-BbwoUscBNPsMKh8IvLAcWP2x7Oe8ddaiOVdWSV8chsQzwzlp-kI2A4y_Ier-Qnns8j2-PYfsASxlP8zRRgMZq"/>
              </div>
              <div class="flex-1">
                <p class="text-xs font-bold leading-tight">The Midnight Library</p>
                <p class="text-[10px] text-gray-500 dark:text-gray-400 mb-2">Matt Haig</p>
                <UProgress :value="85" size="xs" color="primary" />
                <p class="text-[9px] text-gray-500 dark:text-gray-400 mt-1 font-medium italic">Position #2 • Approx. 4 days</p>
              </div>
            </div>

            <div class="flex gap-4">
              <div class="w-12 h-16 bg-gray-200 dark:bg-gray-700 rounded overflow-hidden flex-shrink-0 opacity-80">
                <img alt="Foundation's Edge" class="w-full h-full object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuAwL0hqZ95dvM4sPO40eFOATU6myRscp3Pp1sEoZKuoZxZEwMZmxpQpCqikaSsaTdBTm9S6Vf-C-EZjm_FQrMVcUfgTUQk_xZmuCafeC0TPSvzY2nqUtaPkSwFYIfgbLi9xbrzQEy9C7OgsTm6gABr-jovCLjz5KORMF1pPfrzdN7mEkTqaAk638IfQFNCAOwAxSIjUqGxRZNEN80E0HkgDU45Wm-PlYLX1xy0mrll2fp9ZHgrRNyAGdM5JKQ32CazQFkNR_Hgh0xmQ"/>
              </div>
              <div class="flex-1">
                <p class="text-xs font-bold leading-tight">Foundation's Edge</p>
                <p class="text-[10px] text-gray-500 dark:text-gray-400 mb-2">Isaac Asimov</p>
                <UProgress :value="20" size="xs" color="gray" />
                <p class="text-[9px] text-gray-500 dark:text-gray-400 mt-1 font-medium italic">Position #14 • Approx. 3 weeks</p>
              </div>
            </div>

            <div class="flex gap-4">
              <div class="w-12 h-16 bg-gray-200 dark:bg-gray-700 rounded overflow-hidden flex-shrink-0 opacity-80">
                <img alt="Project Hail Mary" class="w-full h-full object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuBoG7XQq5hXY9Ra7LIP6JPWmYXqYm4DppnHLwmSoDkq50ph_XQJzMdQszNFllUxGmsdljtPpFkl7qWGTFZKeOehel9gIAvc0xvuF5GCkVd951HKgjAPOaXBdqNMuY_-2eB1NBXdRyEFmVALQskWRwMkfoRtF3brLfqFnKBhyVOFiq1V4spV3lglXzz3U3RL8GLLEhJ2nFGIG3HLC2iwgDo4P7MqHb9WqoU1GyV8KrG6D7U2KpdI35HPFPxBZL6E-rH-50ctfuzzEjli"/>
              </div>
              <div class="flex-1">
                <p class="text-xs font-bold leading-tight">Project Hail Mary</p>
                <p class="text-[10px] text-gray-500 dark:text-gray-400 mb-2">Andy Weir</p>
                <UProgress :value="5" size="xs" color="gray" />
                <p class="text-[9px] text-gray-500 dark:text-gray-400 mt-1 font-medium italic">Position #42 • Estimated Oct 12</p>
              </div>
            </div>
            
          </div>
          
          <template #footer>
            <UButton block color="gray" variant="ghost" class="uppercase text-[10px] font-bold tracking-widest mt-2 bg-gray-200 dark:bg-gray-700/50 hover:bg-gray-300 dark:hover:bg-gray-700">
              Manage All Reservations
            </UButton>
          </template>
        </UCard>
      </div>

      <!-- Currently Borrowed Books -->
      <div class="md:col-span-8">
        <UCard class="shadow-sm architect-shadow">
          <template #header>
            <div class="flex justify-between items-center">
              <h2 class="text-sm font-bold uppercase tracking-tight">Currently Borrowed</h2>
              <UButton variant="link" color="primary" class="font-bold text-xs" size="sm">Extend All Loans</UButton>
            </div>
          </template>

          <div class="grid grid-cols-1 sm:grid-cols-2 gap-8">
            <!-- Book Card 1 -->
            <div class="flex flex-col">
              <div class="relative group mb-4">
                <div class="aspect-[3/4] rounded-lg overflow-hidden bg-gray-100 dark:bg-gray-800">
                  <img alt="The Alchemist" class="w-full h-full object-cover transition-transform duration-500 group-hover:scale-105" src="https://lh3.googleusercontent.com/aida-public/AB6AXuCqLQR5RrxyrjlxKiN71rMgxDwKhmBCZanQRFBzjaNqAUoImAkBoIdW2uTQ8FE08d4VFOcd56BsDHhtRbimFyXahCvV-6Nhb2LyT7ssV81mRke7qX28XWEffk8rvdETgkkyiM01EH5n6PJ8POdy96lyko4FlPIaeqOxlzLmd3rcGQsdbkBuxDrRitriSndYZ6-my265K1TTtVIwCqj6J0NsqzBAVBIIXC0k7bmrLlcifbXgFCqy8km8mPj93qqu_-WhG4SzV6YliPa_"/>
                </div>
                <div class="absolute top-3 left-3 bg-red-600 text-white text-[9px] font-black px-2 py-1 rounded shadow-lg uppercase tracking-tighter">
                  Due in 2 days
                </div>
              </div>
              <div>
                <h3 class="font-bold text-base">The Alchemist</h3>
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-4">Paulo Coelho • Borrowed Aug 14</p>
                <div class="flex gap-2">
                  <UButton color="primary" class="flex-1 justify-center font-bold" size="sm">Return Now</UButton>
                  <UButton variant="outline" color="gray" icon="i-lucide-more-horizontal" size="sm" />
                </div>
              </div>
            </div>

            <!-- Book Card 2 -->
            <div class="flex flex-col">
              <div class="relative group mb-4">
                <div class="aspect-[3/4] rounded-lg overflow-hidden bg-gray-100 dark:bg-gray-800">
                  <img alt="Deep Work" class="w-full h-full object-cover transition-transform duration-500 group-hover:scale-105" src="https://lh3.googleusercontent.com/aida-public/AB6AXuC_fpL97h3GokKT6-mbYjfg3629ECMqdUlC6l2JadXm797yfHhuC7BIagf76jJqhqsIKocWILBPFzoCDduhh4guBBxvRC7c6aCCHY7a1as6cFpPydNJbfG4qqEPiNNamm_im2L_Y6vNrpWkrvXy-9vLQsunOj9m9U8Zru6x3AXvIqK_vsYBw3LdjDHBzvW-iJPQ01t5H5TFvVkwEqcajM_C_rBaAkO3lbcdZm7ttC7zTiU9nd_uJYUB3aDqMYWZhZKQedIX03GLKa-z"/>
                </div>
                <div class="absolute top-3 left-3 bg-emerald-600 text-white text-[9px] font-black px-2 py-1 rounded shadow-lg uppercase tracking-tighter">
                  Healthy
                </div>
              </div>
              <div>
                <h3 class="font-bold text-base">Deep Work</h3>
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-4">Cal Newport • Borrowed Aug 22</p>
                <div class="flex gap-2">
                  <UButton color="primary" class="flex-1 justify-center font-bold" size="sm">Renew Book</UButton>
                  <UButton variant="outline" color="gray" icon="i-lucide-more-horizontal" size="sm" />
                </div>
              </div>
            </div>
          </div>
        </UCard>
      </div>

      <!-- Reading Progress / Insights -->
      <div class="md:col-span-8 bg-gradient-to-br from-primary-600 to-primary-800 dark:from-primary-700 dark:to-primary-900 p-8 rounded-xl text-white flex flex-col md:flex-row items-center justify-between gap-8 architect-shadow">
        <div class="space-y-4 max-w-md">
          <span class="inline-block bg-white/20 backdrop-blur px-3 py-1 rounded-full text-[10px] font-bold uppercase tracking-widest">Monthly Insight</span>
          <h2 class="text-2xl font-bold leading-tight">You're in the top 5% of readers this month in your district.</h2>
          <p class="text-primary-100 text-sm font-medium">Keep reading to unlock the "Master Archivist" badge and get priority access to new releases.</p>
        </div>
        <div class="relative w-32 h-32 flex flex-shrink-0 items-center justify-center">
          <svg class="w-full h-full transform -rotate-90">
            <circle class="text-white/10" cx="64" cy="64" fill="transparent" r="56" stroke="currentColor" stroke-width="8"></circle>
            <circle class="text-green-300" cx="64" cy="64" fill="transparent" r="56" stroke="currentColor" stroke-dasharray="351.85" stroke-dashoffset="87.96" stroke-width="8" stroke-linecap="round"></circle>
          </svg>
          <div class="absolute inset-0 flex flex-col items-center justify-center">
            <span class="text-2xl font-black">75%</span>
            <span class="text-[8px] uppercase tracking-widest font-bold">Goal</span>
          </div>
        </div>
      </div>

      <!-- Recommended (Asymmetric Element) -->
      <div class="md:col-span-4 bg-gray-100 dark:bg-gray-800 p-6 rounded-xl relative overflow-hidden flex flex-col justify-end min-h-[240px] architect-shadow border border-gray-200 dark:border-gray-700">
        <img alt="Atmospheric Library" class="absolute inset-0 w-full h-full object-cover opacity-30 dark:opacity-20" src="https://lh3.googleusercontent.com/aida-public/AB6AXuDe4Er99CRroEig6hcr_C8_tD9LxSgz3KAHwzgfQalWC2SO5MGDBxyNDdphTuhi9eu3naHFsnSleG1FuBg30CgvKFPzsgCFBRQyxJxRVL-zLTIoRrKvuIGdu853diZOYm4-A22BFUSmGfcJAE1j9QV6h1ChM8wgD8wHKoVe__7WkCGxc5wXB08csVytNDk_i95i63_RQx93m9WsFp3aWIgerrvl5YPrauzR-i_EnReeGK7syUwjawsNCra0GBnHCSxWzbMOmu4uEXhg"/>
        <div class="relative z-10 p-4 bg-white/70 dark:bg-black/50 backdrop-blur rounded-lg shadow-sm border border-white/20 dark:border-gray-700/50">
          <h3 class="text-lg font-bold mb-2">Curated for you</h3>
          <p class="text-xs text-gray-700 dark:text-gray-300 mb-4">Based on your interest in Sci-Fi and Productivity journals.</p>
          <UButton variant="ghost" color="primary" class="p-0 font-bold uppercase tracking-widest text-[10px]" trailing-icon="i-lucide-arrow-right">
            Explore Feed
          </UButton>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
definePageMeta({
  layout: 'dashboard'
})

useSeoMeta({
  title: 'Member Dashboard',
})
</script>

<style scoped>
.architect-shadow {
  box-shadow: 0 12px 32px -4px rgba(0, 0, 0, 0.08);
}
.dark .architect-shadow {
  box-shadow: 0 12px 32px -4px rgba(0, 0, 0, 0.4);
}
</style>
