<script setup>
const activeTab = ref('member')

definePageMeta({
  layout: false
})

useHead({
  link: [
    { rel: 'preconnect', href: 'https://fonts.googleapis.com' },
    { rel: 'preconnect', href: 'https://fonts.gstatic.com', crossorigin: '' },
    { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap' },
    { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&display=swap' }
  ],
  style: [
    {
      children: `
        .material-symbols-outlined {
          font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
          vertical-align: middle;
        }
        .glow-border {
          position: relative;
        }
        .glow-border::after {
          content: '';
          position: absolute;
          inset: -1px;
          background: linear-gradient(to bottom right, #10b981 0%, transparent 50%, #10b981 100%);
          border-radius: inherit;
          z-index: -1;
          opacity: 0.15;
        }
      `
    }
  ]
})
</script>

<template>
  <div class="bg-slate-950 text-slate-50 min-h-screen flex items-center justify-center relative overflow-hidden font-['Inter',sans-serif]">
    <!-- Background Elements -->
    <div class="absolute inset-0 overflow-hidden">
      <div class="absolute -top-1/4 -right-1/4 w-[600px] h-[600px] bg-emerald-500/10 rounded-full blur-[120px]"></div>
      <div class="absolute -bottom-1/4 -left-1/4 w-[600px] h-[600px] bg-emerald-900/20 rounded-full blur-[120px]"></div>
    </div>
    <main class="w-full max-w-md px-6 relative z-10">
      <!-- Branding Header -->
      <div class="text-center mb-8">
        <div class="inline-flex items-center justify-center w-14 h-14 bg-emerald-500/10 border border-emerald-500/20 rounded-xl mb-4">
          <span class="material-symbols-outlined text-emerald-500 text-3xl">settings_input_antenna</span>
        </div>
        <h1 class="text-2xl font-bold tracking-tight text-white">PearlLibrary</h1>
        <p class="text-slate-400 text-sm mt-1">Access the ethereal collection</p>
      </div>
      <!-- Login Portal Card -->
      <div class="bg-slate-900/50 backdrop-blur-xl border border-slate-800 rounded-xl p-8 shadow-2xl glow-border">
        <!-- Unified Portal Tabs -->
        <div class="flex p-1 bg-slate-900/50 rounded-lg mb-8 border border-slate-800">
          <button 
            @click="activeTab = 'member'"
            :class="[
              'flex-1 py-1.5 text-xs font-semibold rounded-md transition-all',
              activeTab === 'member' ? 'bg-emerald-500 text-white shadow-sm' : 'text-slate-400 hover:text-white font-medium bg-transparent'
            ]"
          >
            Member Portal
          </button>
          <button 
            @click="activeTab = 'staff'"
            :class="[
              'flex-1 py-1.5 text-xs font-semibold rounded-md transition-all',
              activeTab === 'staff' ? 'bg-emerald-500 text-white shadow-sm' : 'text-slate-400 hover:text-white font-medium bg-transparent'
            ]"
          >
            Staff Access
          </button>
        </div>
        <form action="#" class="space-y-5">
          <!-- Input Group: Email -->
          <div class="space-y-1.5">
            <label class="block text-xs font-medium text-slate-400" for="identity">Email or Username</label>
            <div class="relative group">
              <div class="absolute inset-y-0 left-0 pl-3.5 flex items-center pointer-events-none">
                <span class="material-symbols-outlined text-slate-400 group-focus-within:text-emerald-500 transition-colors text-lg">alternate_email</span>
              </div>
              <input class="block w-full pl-10 pr-4 py-2.5 bg-slate-900/50 border border-slate-800 rounded-lg text-sm text-slate-50 placeholder:text-slate-600 focus:ring-1 focus:ring-emerald-500 focus:border-emerald-500 transition-all outline-none" id="identity" name="identity" placeholder="e.g. curator@pearl.lib" type="text"/>
            </div>
          </div>
          <!-- Input Group: Password -->
          <div class="space-y-1.5">
            <div class="flex justify-between items-center">
              <label class="block text-xs font-medium text-slate-400" for="password">Password</label>
              <a class="text-xs font-medium text-emerald-500 hover:text-emerald-400 transition-colors" href="#">Forgot password?</a>
            </div>
            <div class="relative group">
              <div class="absolute inset-y-0 left-0 pl-3.5 flex items-center pointer-events-none">
                <span class="material-symbols-outlined text-slate-400 group-focus-within:text-emerald-500 transition-colors text-lg">lock_open</span>
              </div>
              <input class="block w-full pl-10 pr-4 py-2.5 bg-slate-900/50 border border-slate-800 rounded-lg text-sm text-slate-50 placeholder:text-slate-600 focus:ring-1 focus:ring-emerald-500 focus:border-emerald-500 transition-all outline-none" id="password" name="password" placeholder="••••••••" type="password"/>
            </div>
          </div>
          <!-- Options Row -->
          <div class="flex items-center">
            <div class="flex h-5 items-center">
              <input class="h-4 w-4 rounded border-slate-800 bg-slate-900 text-emerald-500 focus:ring-emerald-500 focus:ring-offset-slate-900" id="remember" type="checkbox"/>
            </div>
            <label class="ml-2 text-xs font-medium text-slate-400 cursor-pointer" for="remember">Stay signed in for 30 days</label>
          </div>
          <!-- CTA Action -->
          <button class="w-full bg-emerald-500 hover:bg-emerald-600 text-white py-2.5 px-4 rounded-lg font-semibold text-sm transition-all active:scale-[0.98] shadow-lg shadow-emerald-500/20" type="submit">
            Sign In
          </button>
        </form>
        <!-- Footer Help -->
        <div class="mt-6 text-center pt-6 border-t border-slate-800">
          <p class="text-xs text-slate-400">
            New to the library? 
            <a class="text-emerald-500 font-semibold ml-1 hover:text-emerald-400 transition-colors" href="#">Request an account</a>
          </p>
        </div>
      </div>
      <!-- External Links -->
      <div class="mt-8 flex justify-center gap-6 text-[10px] font-bold uppercase tracking-widest text-slate-400/40">
        <a class="hover:text-emerald-500 transition-colors" href="#">Privacy Policy</a>
        <a class="hover:text-emerald-500 transition-colors" href="#">Terms of Use</a>
        <a class="hover:text-emerald-500 transition-colors" href="#">Support</a>
      </div>
    </main>
    <!-- Visual Feature Image -->
    <div class="hidden lg:block absolute bottom-12 right-12 w-64 h-80 overflow-hidden rounded-2xl shadow-2xl border border-slate-800 rotate-2 transition-transform hover:rotate-0 duration-500">
      <img alt="Botanical Library" class="w-full h-full object-cover grayscale-[0.2]" src="https://lh3.googleusercontent.com/aida-public/AB6AXuAGDMhMpEGdKCgCdbJvLMTZhLxxbz09EwMb2vdHTMK8JaP0nkZmXXI98F1XJX0m3eSVwyP3ixXcPdt_jSdDPWuxRCvz91rhdlYaybrqLJ0X7dzIpa_cQthADlQKI1Tgq451b3KzAHKTMK7uv_CEzFBL6ixB4M1caSQ2xmrA2SoaGauXQsjzVrQjE4f-SkIgH9SBHS5wBsPSSBx7losBReYk3PD8bohNmhgYlspg7Al0y7SbBxuPPbC0uflPJ4Z88bKnNlJd0TrEUXca"/>
      <div class="absolute inset-0 bg-gradient-to-t from-slate-950/90 via-slate-950/20 to-transparent flex flex-col justify-end p-5">
        <p class="text-white font-medium text-sm italic">"A sanctuary for the soul's curiosity."</p>
      </div>
    </div>
  </div>
</template>
