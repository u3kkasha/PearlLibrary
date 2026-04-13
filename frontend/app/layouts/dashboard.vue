<template>
  <div class="min-h-screen bg-gray-50 dark:bg-gray-950 text-gray-900 dark:text-gray-100 transition-colors duration-200">
    <!-- TopNavBar -->
    <header class="w-full h-16 sticky top-0 z-50 bg-white/80 dark:bg-gray-900/80 backdrop-blur border-b border-gray-200 dark:border-gray-800 flex items-center justify-between px-6">
      <div class="flex items-center gap-4 md:gap-8">
        <UButton
          icon="i-lucide-menu"
          color="gray"
          variant="ghost"
          class="hidden md:flex"
          aria-label="Toggle Navigation"
          @click="isSidebarCollapsed = !isSidebarCollapsed"
        />
        <span class="text-xl font-bold tracking-tight text-primary-600 dark:text-primary-400">Lumina Library</span>
        <div class="hidden md:flex items-center">
          <UInput
            icon="i-lucide-search"
            placeholder="Search catalog..."
            color="gray"
            variant="ghost"
            class="w-64 bg-gray-100 dark:bg-gray-800 rounded-xl"
            :ui="{ icon: { list: { pointerEvents: 'none' } } }"
          />
        </div>
      </div>
      <div class="flex items-center gap-4">
        <div class="flex items-center gap-2 mr-4">
          <ClientOnly>
            <UButton icon="i-lucide-sun" color="gray" variant="ghost" class="dark:hidden" aria-label="Theme toggle" @click="$colorMode.preference = 'dark'" />
            <UButton icon="i-lucide-moon" color="gray" variant="ghost" class="hidden dark:flex" aria-label="Theme toggle" @click="$colorMode.preference = 'light'" />
          </ClientOnly>

          <UButton icon="i-lucide-bell" color="gray" variant="ghost" class="relative">
            <template #trailing>
              <span class="absolute top-1.5 right-1.5 w-2 h-2 bg-red-500 rounded-full"></span>
            </template>
          </UButton>
          <UButton icon="i-lucide-settings" color="gray" variant="ghost" />
        </div>
        <div class="flex items-center gap-3 pl-4 border-l border-gray-200 dark:border-gray-800">
          <div class="text-right hidden sm:block">
            <p class="text-xs font-bold">Elena Vance</p>
            <p class="text-[10px] text-primary-600 dark:text-primary-400 uppercase tracking-wider">Premium Member</p>
          </div>
          <UAvatar src="https://lh3.googleusercontent.com/aida-public/AB6AXuBuwRCMqSLsB57xdrTLo7cuP4JQI3V2y5L8yFOvh6wVzUUTNwYyEJJ70s4WgV5Q82KEhDNV_IHX-dM7z5H5RpSLx1R6ZrjTL01Fl3TbyHk-8E13rVTYdN9FusI_2gs0XJIymOuLi2Yyv58HWN-bpOPQhrkxNhStYK9bX5gwvhlumzsgsWWiPvE8FMcW9_ctlmCyavkVTf-yzNPG4dPCUjoVc0271d4plaPVHmcr0OKj_1hW7XX7fCLKLM4CM79d64WyM8a3n3kJ04XI" alt="Elena Vance" size="md" class="border-2 border-primary-200 dark:border-primary-800" />
        </div>
      </div>
    </header>

    <div class="flex min-h-[calc(100vh-64px)]">
      <!-- SideNavBar -->
      <aside 
        :class="['hidden md:flex flex-col h-[calc(100vh-64px)] sticky top-16 bg-gray-100 dark:bg-gray-900 py-8 border-r border-gray-200 dark:border-gray-800 transition-all duration-300', isSidebarCollapsed ? 'w-20' : 'w-64']"
      >
        <nav class="flex-1 px-4 space-y-2">
          <UButton to="#" block align="left" icon="i-lucide-layout-dashboard" color="primary" variant="soft" class="font-bold justify-start" :ui="{ icon: { base: 'flex-shrink-0' } }">
            <span v-if="!isSidebarCollapsed" class="text-xs uppercase tracking-wider truncate">Dashboard</span>
          </UButton>
          <UButton to="#" block align="left" icon="i-lucide-book-open" color="gray" variant="ghost" class="justify-start" :ui="{ icon: { base: 'flex-shrink-0' } }">
            <span v-if="!isSidebarCollapsed" class="text-xs uppercase tracking-wider truncate">Catalog</span>
          </UButton>
          <UButton to="#" block align="left" icon="i-lucide-history" color="gray" variant="ghost" class="justify-start" :ui="{ icon: { base: 'flex-shrink-0' } }">
            <span v-if="!isSidebarCollapsed" class="text-xs uppercase tracking-wider truncate">History</span>
          </UButton>
          <UButton to="#" block align="left" icon="i-lucide-library" color="gray" variant="ghost" class="justify-start" :ui="{ icon: { base: 'flex-shrink-0' } }">
            <span v-if="!isSidebarCollapsed" class="text-xs uppercase tracking-wider truncate">Inventory</span>
          </UButton>
          <UButton to="#" block align="left" icon="i-lucide-bar-chart-2" color="gray" variant="ghost" class="justify-start" :ui="{ icon: { base: 'flex-shrink-0' } }">
            <span v-if="!isSidebarCollapsed" class="text-xs uppercase tracking-wider truncate">Analytics</span>
          </UButton>
        </nav>
        <div class="px-4 mt-auto space-y-2">
          <UButton to="#" block align="left" icon="i-lucide-help-circle" color="gray" variant="ghost" class="justify-start" :ui="{ icon: { base: 'flex-shrink-0' } }">
            <span v-if="!isSidebarCollapsed" class="text-xs uppercase tracking-wider truncate">Help</span>
          </UButton>
          <UButton to="#" block align="left" icon="i-lucide-log-out" color="gray" variant="ghost" class="justify-start" :ui="{ icon: { base: 'flex-shrink-0' } }">
            <span v-if="!isSidebarCollapsed" class="text-xs uppercase tracking-wider truncate">Logout</span>
          </UButton>
        </div>
      </aside>

      <!-- Main Content -->
      <main class="flex-1 p-8 overflow-y-auto">
        <slot />
      </main>
    </div>

    <!-- Mobile Navigation Shell -->
    <nav class="md:hidden fixed bottom-0 left-0 right-0 h-16 bg-white dark:bg-gray-900 border-t border-gray-200 dark:border-gray-800 px-6 flex items-center justify-between z-50">
      <UButton variant="ghost" color="primary" class="flex-col items-center gap-1" :ui="{ padding: { sm: 'p-1' } }">
        <UIcon name="i-lucide-layout-dashboard" class="text-xl" />
        <span class="text-[10px]">Home</span>
      </UButton>
      <UButton variant="ghost" color="gray" class="flex-col items-center gap-1" :ui="{ padding: { sm: 'p-1' } }">
        <UIcon name="i-lucide-book-open" class="text-xl" />
        <span class="text-[10px]">Catalog</span>
      </UButton>
      <UButton variant="ghost" color="gray" class="flex-col items-center gap-1" :ui="{ padding: { sm: 'p-1' } }">
        <UIcon name="i-lucide-history" class="text-xl" />
        <span class="text-[10px]">History</span>
      </UButton>
      <UButton variant="ghost" color="gray" class="flex-col items-center gap-1" :ui="{ padding: { sm: 'p-1' } }">
        <UIcon name="i-lucide-library" class="text-xl" />
        <span class="text-[10px]">Inv</span>
      </UButton>
      <UButton variant="ghost" color="gray" class="flex-col items-center gap-1" :ui="{ padding: { sm: 'p-1' } }">
        <UIcon name="i-lucide-bar-chart-2" class="text-xl" />
        <span class="text-[10px]">Stats</span>
      </UButton>
    </nav>

    <!-- FAB for quick book search/scan -->
    <UButton
      icon="i-lucide-qr-code"
      color="primary"
      class="fixed bottom-24 right-6 md:bottom-10 md:right-10 w-14 h-14 rounded-full flex items-center justify-center architect-shadow active:scale-95 transition-transform z-40 !p-0"
      :ui="{ rounded: 'rounded-full' }"
      size="xl"
    />
  </div>
</template>

<script setup>
const colorMode = useColorMode()
const isSidebarCollapsed = ref(false)
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700;800;900&display=swap');

div, span, p, h1, h2, h3, a, button {
  font-family: 'Inter', sans-serif;
}

.architect-shadow {
  box-shadow: 0 12px 32px -4px rgba(0, 0, 0, 0.08);
}
.dark .architect-shadow {
  box-shadow: 0 12px 32px -4px rgba(0, 0, 0, 0.4);
}
</style>
