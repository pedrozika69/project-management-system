<script>
  import Footer from "$lib/components/Footer.svelte";
  import Sidebar from "$lib/components/Sidebar.svelte";
</script>

<div id="layoutSidenav">
    <div id="layoutSidenav_nav">
        <Sidebar />
    </div>
    <div id="layoutSidenav_content">
        <main>
            <div class="container-fluid px-4">
                <h1 class="mt-4">Dashboard</h1>
                <ol class="breadcrumb mb-4">
                    <li class="breadcrumb-item active">Dashboard</li>
                </ol>
                <slot />
            </div>
        </main>
        <Footer />
    </div>
</div>